# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

BZEditor is a Windows Forms (C#, .NET Framework 4.8) zone editor for the Bylins MUD. Builders use it
to edit a *world* — zones made of rooms, mobs, objects, triggers and zone reset commands — and save it
back as the YAML world format the engine (`bylins/mud`, `yaml_world_data_source.cpp`) reads.
Most UI strings, comments and game data are Russian.

## Build & test

MSBuild only — this is an old-style (non-SDK) csproj tree with `packages.config`; `dotnet build` will not work.

```sh
# MSBuild is not on PATH here; locate it with vswhere:
MSBUILD=$("C:/Program Files (x86)/Microsoft Visual Studio/Installer/vswhere.exe" -latest -products '*' \
  -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe')

"$MSBUILD" BZEditor.sln -p:Configuration=Debug -m          # Release also valid
```

`packages/` is already restored in this checkout. If a package is missing, run `nuget restore BZEditor.sln`
(needs `nuget.exe`; CI installs it).

Tests are NUnit 3 in `DataUtils.Tests`, run via the console runner against the built DLL:

```sh
packages/NUnit.ConsoleRunner.3.16.3/tools/nunit3-console.exe BZEditorBuild/DataUtils.Tests.dll --noresult
# one fixture / one test:
... --where "class =~ DataUtils.Tests.YamlDiceTests"
... --test=DataUtils.Tests.DataPreservationTests.Obj_ExtraValues_SurviveRoundTrip
```

`ProdWorldIntegrationTests` load a real world and are **ignored** unless `BZED_TEST_WORLD` points at a
directory containing `zones/`. That world is game content and is not in this repo; CI runs without it.

Running the app: `BZEditorBuild/BZEditor.exe`. A post-build target copies `redist/**` (Bases, Configurations,
World, `BZEditorConfig.xml`) next to the exe, so a fresh build is immediately runnable against the bundled
sample world. `BZEditor.csproj` has a pre-build step that renames a locked `BZEditor.exe` to `.locked` so a
running editor doesn't break the build.

## Conventions that bite

- **Encoding.** Source files are UTF-8 **with BOM**, CRLF (`.editorconfig`). World YAML is **written as
  UTF-8 without a BOM**; reads sniff the bytes and fall back to **koi8-r** for worlds written before that
  switch (`YamlEncoding` — `Utf8NoBom`/`Legacy`/`Decode`, used by `YamlFormatProvider` and
  `FileListsDataManager.DiscoverZonesYaml`). The legacy side files (`.zon`, `.shp`, `.skt`, `.gskt`,
  templates) stay on `StaticData.CurrentEncoding` (koi8-r); the `Bases/*.bb` reference files are
  **windows-1251**. Reading a world file with the wrong encoding silently mangles the Russian text.
  `Directory.Build.props` pins `TargetFrameworkVersion=v4.8` and `CodePage=65001` for all projects — don't
  re-set them per-csproj.
- **Adding a file requires a csproj edit.** Non-SDK projects list every source explicitly
  (`<Compile Include="..."/>`); a new `.cs` that isn't listed simply won't compile.
- `WldForm` is one class split across `BZEditor/Forms/WldForm.cs` (~7.7k lines) plus per-tab partials at the
  project root: `WldForm.Wld.cs` (rooms/map), `.Mob.cs`, `.Obj.cs`, `.Trg.cs`, `.Zon.cs`, `.Shp.cs`,
  `.Templates.cs`, `.Navigation.cs`. Tab-specific UI code belongs in the matching partial.
- `Main()` lives in `BZEditor/Forms/MainForm.cs`, not a `Program.cs`.

## Architecture

Five projects (`BZEditor.sln`), all output to `BZEditorBuild/`:

- **DataUtils** — the model and all file I/O. No UI dependency; this is what the tests target.
- **BZEditor** — WinForms UI (DockPanelSuite for docking, ScintillaNET for the DG Script editor).
- **ExtControls** — custom controls, notably `WldMap`/`WldSketch` (the room-grid map canvas) and `ExtListView`.
- **SystemFrameworks** — exception plumbing (`BZedException`, `BZedExceptionCatcher`, `ExceptionForm`) plus string/binary helpers.
- **DataUtils.Tests** — NUnit.

### World data flow

`ZoneDataManager` (`DataUtils/ZoneDataManager.cs`) is the unit of work: one instance per open zone, owning
that zone's `MobsCollection`, `ObjsCollection`, `RoomsCollection`, `TriggersCollection`, `SketchRoomsCollection`
and `Zone`. It delegates every read/write to an `IFormatProvider`.

`IFormatProvider` / `BaseFormatProvider` define Load*/Save* per entity kind. Today there is exactly one
implementation, **`YamlFormatProvider`** — the CircleMUD text format was dropped (commit `0c02ecd`) along with
the format picker. Layout on disk:

```
<world>/world_config.yaml          # layout: flat, line_endings: unix — rewritten on save
<world>/zones/<zoneNum>/zone.yaml  # zone header + `commands:` reset list
                       rooms.yaml  # flat map: relNum -> body   (vnum = zone*100 + rel)
                       mobs.yaml objects.yaml triggers.yaml
<world>/zones/index.yaml           # list of zone vnums
```

A legacy per-file layout (`rooms/NN.yaml` + `index.yaml`) is still *readable*; the editor always *writes* flat.
Zone discovery (`FileListsDataManager.DiscoverZonesYaml`) is just "a `zones/<n>/` dir containing `zone.yaml`".

**`ZoneFileManager` is not a format provider.** It is the old CircleMUD `.zon` serializer, kept alive solely
because `YamlFormatProvider.PopulateZoneCommands`/`LoadZoneCommands` reuse it: reset commands are round-tripped
by writing a throwaway `.zon` into a temp dir and parsing the command lines back out. Touching zone reset
commands means touching both files.

### Domain to YAML translation

Three layers, all under `DataUtils/`:

- `DataObjects/*` — the editor's in-memory model (`Mob`, `Obj`, `Room`, `Trigger`, `Zone` and their
  `*Collection` types, mostly keyed by `VNum`, deriving from `BaseDataObject`).
- `YamlModels/*` — DTOs shaped exactly like the engine's YAML (snake_case via `UnderscoredNamingConvention`),
  plus custom `IYamlTypeConverter`s (`NamedIntMapConverter`, `MobSpellMapConverter`).
- `YamlMappers/*` — `Yaml<Entity>Mapper.ToYaml/FromYaml` between the two, and:
  - `EngineCodec` — converts the editor's bitflags to/from the engine's symbolic `kXxx` names and decodes the
    legacy "asciiflag" encoding (letter+plane-digit pairs; `bit = letterValue + 30*plane`).
  - `EngineDictionaries` — the `kName -> value` tables generated from the engine's `world/dictionaries/*.yaml`.

Two rules the serializer setup encodes, both load-bearing:

1. **Do not omit default scalars on save.** The engine applies its own (sometimes non-zero) defaults for
   missing keys, so writing a `0` is not the same as omitting it. Only nulls and empty collections are omitted.
   This also keeps the editor's own round-trip idempotent.
2. **Read both old and new shapes, write the new one.** Mob resistances/saves appear as positional lists in
   older worlds and named maps in current ones (`NamedIntMapTests`); mob spells were a list of ids and are now
   id → count (`MobSpellMapConverter`, commits `f326e17` / `7baf2fc`).

`DataPreservationTests` exists because fields were being silently dropped on save. When adding a field to a
model, add a round-trip assertion there — it runs in CI with no world data.

`ZoneDataManagerDS` is a separate, `DataTable`-based view of the same zone used to feed list views; it
duplicates parsing in places. `TemplatesDataManager`, `SetsFileManager`, `ShopsFileManager` and
`GlobalSketchFileManager` handle their own side files.

### Reference data ("Bases")

`redist/Bases/*.bb` (copied to `BZEditorBuild/Bases`) are windows-1251 lookup tables — flag names, spell and
skill lists, sector types, DG Script autocomplete. `BZEditor/CBasesDataManager.cs` parses them into
`DataTable`s, dispatching by filename through hardcoded arrays (`twoParamsFiles`, `grouppedTwoParamsFiles`,
`grouppedFiveParamsFiles`, …), so adding a `.bb` file means adding its name to the right array. These drive
the UI's dropdowns and checklists; `EngineDictionaries` drives what gets written to disk — separate sources of
truth that can drift.

### Cross-cutting statics

`DataUtils/Static.cs` holds process-global state most code reads implicitly: `StaticData.WorldFolderPath`,
`StaticData.CurrentEncoding`, `ConfigFolder`, `MaxRoomsPerZone` (98), `MinZ`/`MaxZ`, `BackupZones`, and
`CanFireChangeEvent` (must be toggled when switching the active zone tab, or change events fire for the wrong
zone). Tests set `WorldFolderPath`/`CurrentEncoding` directly. Code that temporarily retargets
`WorldFolderPath` (the `.zon` round-trip) must restore it in a `finally`.

Errors surface through `BaseFileManager.ExceptionThrowed` (`message, Exception, EventLogEntryType`), which
providers and managers re-raise up to the UI; failures during a save are generally reported as warnings rather
than aborting the save.

App settings live in `BZEditorConfig.xml` next to the exe (`BZEditor/Utils/Configuration.cs`), including
`PathToWorldFolder` (relative paths resolve against the exe folder) and `OpenedZonesList`.

## CI

`.github/workflows/build.yml` on windows-latest: nuget restore → `msbuild -p:Configuration=Release` → nunit
console → assemble `dist/BZEditor` (binaries minus test/nunit assemblies, plus all of `redist/`) → upload
artifact. A `v*` tag additionally zips it and publishes a GitHub release.

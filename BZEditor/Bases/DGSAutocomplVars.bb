*Встроенные переменные
self	self	DarkBlue	UID	Получение владельца триггера	Владелец триггера	1
exist.mob	exist.mob (vnum)	DarkBlue	UID	Получение владельца триггера	1 – моб существует, 0 – странный результат, nil – моба не существует	1
exist.obj	exist.obj (vnum)	DarkBlue	число	Получение владельца триггера	1 – предмет существует, 0 – странный результат, nil – предмета не существует	1
world.curobjs	world.curobjs (vnum)	DarkBlue	число	Количество vmun-предметов в мире(в игре в текущий момент и наренте)	Количество предметов	1
world.gameobjs	world.gameobjs (vnum)	DarkBlue	число	Количество vmun-предметов в мире (в игре в текущий момент)	Количество предметов	1
world.people	world.people(vmun)	DarkBlue	число	Количество персонажей в комнате vnum (PC, NPC и др)	Количество персонажей (-1 если комнаты не существует)	1
world.curmobs	world.curmobs(vmun)	DarkBlue	число	Количество vnum-мобов в мире в текущий момент	Количество мобов	1
world.zreset	world.zreset (vnum)	DarkBlue	нет	Вызов процедуры ресета зоны vnum	nil	1
world.mob	world.mob (vnum)	DarkBlue	число	Получение численного значения UID персонажа VNUM	Численное значение UID	1
world.obj	world.obj (vnum)	DarkBlue	число	Получение численного значения UID пердмета VNUM	Численное значение UID	1
world.room	world.room (vnum)	DarkBlue	число	Получение численного значения UID комнаты VNUM	Численное значение UID	1
weather.temp	weather.temp	DarkBlue	число	Температура на дворе	Температура	1
weather.sky	weather.sky	DarkBlue	число	Облачность в мире	0 - облачно, 1 - пасмурно, 2 - тяжелые тучи, 3 - ясно	1
weather.sky	weather.sky (vnum)	DarkBlue	число	Облачность в комнате VNUM	0 - облачно, 1 - пасмурно, 2 - тяжелые тучи, 3 - ясно	1
weather.moon	weather.moon	DarkBlue	число	Возраст луны	Возраст в днях	1
weather.type	weather.type	DarkBlue	строка	Множество букв, описывающее текущую погоду в мире или в комнате VNUM соответственно	a - резкое похолодание,b - резкое потепление,c - моросящий дождь,d - дождь,e - льет как из ведра,f - дождь с градом,g - снежок,h - снегопад,i - валит снег,j - ветерок,k - умеренный ветер,l - сильный ветер	1
weather.type	weather.type (vnum)	DarkBlue	строка	Множество букв, описывающее текущую погоду в мире или в комнате VNUM соответственно	a - резкое похолодание,b - резкое потепление,c - моросящий дождь,d - дождь,e - льет как из ведра,f - дождь с градом,g - снежок,h - снегопад,i - валит снег,j - ветерок,k - умеренный ветер,l - сильный ветер	1
time.hour	time.hour	DarkBlue	число	Игровое время, час	Игровой час	1
time.day	time.day	DarkBlue	число	Игровое время, день	Игровой день	1
time.month	time.month	DarkBlue	число	Игровое время, месяц	Игровой месяц	1
time.year	time.year	DarkBlue	число	Игровое время, год	Игровой год	1
random.char	random.char	DarkBlue	UID	Случайный выбор (выбор из всех)  персонажа в комнате. В поиск не включаются self, NOHASSLE и невидимые для моба/объекта персонажи.	Случайно выбранный персонаж	1
random.pc	random.pc	DarkBlue	UID	Случайный выбор (выбор только из pc)  персонажа в комнате. В поиск не включаются self, NOHASSLE и невидимые для моба/объекта персонажи.	Случайно выбранный персонаж	1
random.npc	random.npc	DarkBlue	UID	Случайный выбор (выбор только из npc)  персонажа в комнате. В поиск не включаются self, NOHASSLE и невидимые для моба/объекта персонажи.	Случайно выбранный персонаж	1
random.num	random.num	DarkBlue	число	Возвращает случайное число	Случайное число	1
random.num	random.num (num)	DarkBlue	число	Возвращает случайное число в диапазоне [1,num]	Случайное число	1
		DarkBlue				1
var.strlen	var.strlen	DarkBlue	число	Вычисление длины строки	Длина строки	1
var.trim	var.trim	DarkBlue	строка	Удаление начальных и конечных пробелов	Урезаная строка	1
var.contains	var.contains (str)	DarkBlue	число	Проверка подстроки	1 - str является подстрокой значения переменной var, 0 - str не является подстрокой значения переменной var	1
var.car	var.car	DarkBlue	строка	Выделение первого слова строки	Первое слово	1
var.cdr	var.cdr	DarkBlue	строка	Выделение части строки после первого слова.	Остаток строки	1
var.words	var.words	DarkBlue	число	Определение количества слов в строке (элементов с списке)	Количество слов	1
var.words	var.words (n)	DarkBlue	строка	Получение n-ого слова строки	Слово	1
var.mudcommand	var.mudcommand	DarkBlue	строка	Получение полной версии команды MUD по значению переменной var. Ищется стандартная MUD команда, аббревиатурой которой является var.	Команда MUD или nil	1
char.iname	char.iname	DarkBlue	строка	Имя (именительный падеж)	Имя	1
char.rname	char.rname	DarkBlue	строка	Имя (родительный падеж)	Имя	1
char.dname	char.dname	DarkBlue	строка	Имя (дательный падеж)	Имя	1
char.vname	char.vname	DarkBlue	строка	Имя (винительный падеж)	Имя	1
char.tname	char.tname	DarkBlue	строка	Имя (творительный падеж)	Имя	1
char.pname	char.pname	DarkBlue	строка	Имя (предложный падеж)	Имя	1
char.name	char.name	DarkBlue	строка	Короткое описание (если есть), иначе имя	Имя	1
char.id	char.id	DarkBlue	число	Получение численного значения UID персонажа char.	Численное значение UID	1
char.alias	char.alias	DarkBlue	строка	Имя	Имя	1
char.level	char.level	DarkBlue	число	Уровень персонажа	Уровень	1
char.hitp	char.hitp	DarkBlue	число	Получение количества hp	Текущее значение hp	1
char.hitp	char.hitp (num)	DarkBlue	число	Изменение hp. Формат num:num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num	Новое значение hp	1
char.maxhitp	char.maxhitp	DarkBlue	число	Получение максимального количества hp	Максимальное количество hp	1
char.mana	char.mana	DarkBlue	число	Получение количества маны	Текущее количество маны	1
char.mana	char.mana (num)	DarkBlue	число	Изменения маны. Формат num:num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num	Новое значение маны	1
char.maxmana	char.maxmana	DarkBlue	число	Получение максимального количества маны	Максимальное количество маны	1
char.move	char.move	DarkBlue	число	Получение количества энергии	Текущее количество энергии	1
char.move	char.move (num)	DarkBlue	число	Изменение энергии. Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num	Новое значение энергии	1
char.maxmove	char.maxmove	DarkBlue	число	Получение максимального количества энергии	Максимальное количество энергии	1
char.align	char.align	DarkBlue	число	GET_ALIGNMENT		1
char.religion	char.religion	DarkBlue	число	Религия персонажа	0 - язычник, 1 - христианин	1
char.gold	char.gold	DarkBlue	число	Получение количества денег	Количество денег	1
char.gold	char.gold (num)	DarkBlue	число	Установка количества денег.Формат num: число - установить значение в num, +число - увеличить значение на num, -число - уменьшить значение на num	Новое количество денег	1
char.bank	char.bank	DarkBlue	число	Получение количества денег в банке	Количество денег в банке	1
char.bank	char.bank (num)	DarkBlue	число	Установка количества денег в банке.Формат num: число - установить значение в num, +число - увеличить значение на num, -число - уменьшить значение на num	Новое количество денег в банке	1
char.exp	char.exp	DarkBlue	число	Получение опыта	Текущий опыт	1
char.exp	char.exp (num)	DarkBlue	число	Изменение опыта. Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num	Новый опыт	1
char.sex	char.sex	DarkBlue	число	Пол персонажа	0 - средний, 1 - мужской, 2 - женский, 3 - мн. число	1
char.clan	char.clan	DarkBlue	строка	Название клана (в нижнем регистре)	Название клана персонажа	1
char.clanrank	char.clanrank	DarkBlue	число	Положение в клане	9 - RANK_KNIEZE,7 - RANK_CENTURION,4 - RANK_VETERAN,2 - RANK_JUNIOR,1 - RANK_NOVICE,0 - RANK_GUEST	1
char.g	char.g	DarkBlue	строка	о//а/и	Суффикс	1
char.u	char.u	DarkBlue	строка	ось/ся/ась/ись	Суффикс	1
char.w	char.w	DarkBlue	строка	ое/ый/ая/ые	Суффикс	1
char.q	char.q	DarkBlue	строка	ло//ла/ли	Суффикс	1
char.y	char.y	DarkBlue	строка	ло/ел/ла/ли	Суффикс	1
char.a	char.a	DarkBlue	строка	о//а/ы	Суффикс	1
char.weight	char.weight	DarkBlue	число	Вес персонажа	Вес персонажа	1
char.canbeseen	char.canbeseen	DarkBlue	число	Проверка видит ли персонаж self, персонаж char.	0 - self не видит персонажа, 1 - self видит персонажа или self не моб	1
char.class	char.class	DarkBlue	число	Класс персонажа	0 - CLERIC,1 - BATTLEMAGE,2 - THIEF,3 - WARRIOR,4 - ASSASINE,5 - GUARD,6 - CHARMMAGE,7 - DEFENDERMAGE,8 - NECROMANCER,9 - PALADINE,10 - RANGER,11 - SMITH,12 - MERCHANT, 13 - DRUID	1
char.race	char.race	DarkBlue	число	Раса персонажа	0 - SEVERANE,1 - POLANE,2 - KRIVICHI,3 - VATICHI,4 - VELANE,5 - DREVLANE	1
char.fighting	char.fighting	DarkBlue	UID	Получение противника в бою	Противник или nil	1
char.is_killer	char.is_killer [(on|off)]	DarkBlue	число	Управление флагом ДУШЕГУБ.on - устанавливает флаг,off - сбрасывает флаг	0 - игрок душегуб,1 - игрок не душегуб	1
char.is_thief	char.is_thief [(on|off)]	DarkBlue	число	Работа с флагом PLR_THIEF	0 - игрок не имеет флага, 1 - игрок имеет флаг	1
char.rentable	char.rentable	DarkBlue	число	Проверка на боевые действия	0 - не может уйти на постой,1 - может уйти на постой	1
char.riding	char.riding	DarkBlue	UID	Определение лошади	Лошадь или nil	1
char.ridden_by	char.ridden_by	DarkBlue	UID	Определение наездника	Наездник или nil	1
char.vnum	char.vnum	DarkBlue	число	vnum моба. ?1 для PC	VNUM	1
char.str	char.str	DarkBlue	число	Врожденная сила персонажа	Сила	1
char.str	char.str (num)	DarkBlue	число	Изменение врожденной силы. num - установить силу равную num, +num - увеличить силу на num, -num - уменьшить силу на num	Сила	1
char.stradd	char.stradd	DarkBlue	число	Добавочная сила персонажа	Сила	1
char.int	char.int	DarkBlue	число	Врожденный ум персонажа	Ум	1
char.int	char.int (num)	DarkBlue	число	Изменение врожденного ума	Ум	1
char.intadd	char.intadd	DarkBlue	число	Добавочный ум персонажа	Ум	1
char.wis	char.wis	DarkBlue	число	Врожденная мудрость персонажа	Мудрость	1
char.wis	char.wis (num)	DarkBlue	число	Изменение врожденной мудрости	Мудрость	1
char.wisadd	char.wisadd	DarkBlue	число	Добавочная мудрость персонажа	Мудрость	1
char.dex	char.dex	DarkBlue	число	Врожденная ловкость персонажа	Ловкость	1
char.dex	char.dex (num)	DarkBlue	число	Изменение врожденной ловкости	Ловкость	1
char.dexadd	char.dexadd	DarkBlue	число	Добавочная ловкость персонажа	Ловкость	1
char.con	char.con	DarkBlue	число	Врожденное тело персонажа	Телосложение	1
char.con	char.con (num)	DarkBlue	число	Изменение врожденного тела	Телосложение	1
char.conadd	char.conadd	DarkBlue	число	Добавочное тело персонажа	Телосложение	1
char.cha	char.cha	DarkBlue	число	Врожденное обаяние персонажа	Обаяние	1
char.cha	char.cha (num)	DarkBlue	число	Изменение врожденного обаяния	Обаяние	1
char.chaadd	char.chaadd	DarkBlue	число	Добавочное обаяние персонажа	Обаяние	1
char.size	char.size	DarkBlue	число	Врожденный размер персонажа	Размер	1
char.size	char.size (num)	DarkBlue	число	Изменение врожденного размера	Размер	1
char.sizeadd	char.sizeadd	DarkBlue	число	Добавочный размер персонажа	Размер	1
char.room	char.room	DarkBlue	число	Получение комнаты, в которой находится персонаж	RNUM комнаты	1
char.realroom	char.realroom	DarkBlue	число	VNUM комнаты, в которой находится персонаж	VNUM комнаты	1
char.loadroom	char.loadroom	DarkBlue	число	Получение загрузочной комнаты	VNUM комнаты	1
char.loadroom	char.loadroom (vnum)	DarkBlue	число	Установка загрузочной комнаты	VNUM комнаты	1
char.skill	char.skill (str)	DarkBlue	число	Уровень умения str у персонажа. Str - название умения	Уровень владения умением	1
char.spellcount	char.spellcount (str)	DarkBlue	число	Количество у персонажа выученных заклинаний str.Str - имя заклинания	Количество заклинаний	1
char.spelltype	char.spelltype (str)	DarkBlue	число	Тип запоминания заклинания str у персонажа. Флаги из массива SplKnw. Str ? имя заклинания	?	1
char.quested	char.quested (num)	DarkBlue	число	Проверка на то, выполнял ли персонаж квест номер num.	1 - выполнял, 0 - не выполнял	1
char.setquest	char.setquest (num)	DarkBlue	число	Установка признака выполнения квеста номер num	1 ? выполнял	1
char.eq	char.eq(pos)	DarkBlue	UID	Получение предмета экипировки.pos - позиция (текст или номер позиции)	Предмет или nil	1
char.haveobj	char.haveobj(obj)	DarkBlue	число	Несет ли персонаж предмет obj? obj может быть vnum или именем предмета.	1 - несет, 0 - не несет	1
char.varexists	char.varexists (name)	DarkBlue	число	Проверяет у сценария объекта char наличие глобальной переменной name.	1 - есть, 0 - нет	1
char.position	char.position	DarkBlue	число	Возвращает положение персонажа	0 - dead,1 - mortally wounded,2 - incapacitated,3 - stunned,4 - sleeping,5 - resting,6 - sitting,7 - fighting,8 - standing	1
char.position	char.position(pos)	DarkBlue	BUG	Устанавливает позицию персонажа. Не действует на богов и т.д.	?	1
char.wait	char.wait	DarkBlue	число	Возвращает лаг персонажа в секундах.	Лаг	1
char.wait	char.wait (pause)	DarkBlue	BUG	Устанавливает лаг персонажа в пульсах. Не действует на богов и т.д.	?	1
char.affect	char.affect (name)	DarkBlue	число	Проверяет наличие на персонаже аффекта name.	1 - аффект есть, 0 - аффекта нет	1
char.leader	char.leader	DarkBlue	UID	Возвращает лидера для char.	Лидер или nil	1
char.people	char.people	DarkBlue	UID	Первый персонаж в комнате с char.	Персонаж или nil	1
char.next_in_room	char.next_in_room	DarkBlue	UID	Следующий за char персонаж в комнате	Персонаж или nil	1
char.all	char.all	DarkBlue	список из UID	все в комнате	Список персонажей, может быть пустым	1
char.char	char.char	DarkBlue	список из UID	все PC и чармисы	Список персонажей, может быть пустым	1
char.pc	char.pc	DarkBlue	список из UID	все PC	Список персонажей, может быть пустым	1
char.npc	char.npc	DarkBlue	список из UID	все NPC (мобы не чармисы)	Список персонажей, может быть пустым	1
char.group	char.group	DarkBlue	список из UID	список группы, в которой состоит char. Начинается с лидера, включает всех последователей.	Список персонажей, может быть пустым	1
char.attackers	char.attackers	DarkBlue	список из UID	список персонажей, атакующих char.	Список персонажей, может быть пустым	1
char.global	char.global (name)	DarkBlue	строка	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария char.	Значение глобальной переменной сценария char	1
char.var 	char.var 	DarkBlue	строка	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария self.	Значение глобальной переменной сценария char	1
obj.iname	obj.iname	DarkBlue	строка	Имя (именительный падеж)	Имя	1
obj.rname	obj.rname	DarkBlue	строка	Имя (родительный падеж)	Имя	1
obj.dname	obj.dname	DarkBlue	строка	Имя (дательный падеж)	Имя	1
obj.vname	obj.vname	DarkBlue	строка	Имя (винительный падеж)	Имя	1
obj.tname	obj.tname	DarkBlue	строка	Имя (творительный падеж)	Имя	1
obj.pname	obj.pname	DarkBlue	строка	Имя (предложный падеж)	Имя	1
obj.name	obj.name	DarkBlue	строка	Имя	Имя	1
obj.id	obj.id	DarkBlue	число	Получение численного значения UID предмета obj.	Численное значение UID	1
obj.shortdesc	obj.shortdesc	DarkBlue	строка	Короткое описание	Описание	1
obj.vnum	obj.vnum	DarkBlue	число	vnum предмета	VNUM	1
obj.type	obj.type	DarkBlue	число	Тип предмета	Код типа предмета	1
obj.timer	obj.timer	DarkBlue	число	Таймер предмета	Таймер	1
obj.val0	obj.val0	DarkBlue	число	Параметр предмета 0	Значение параметра	1
obj.val1	obj.val1	DarkBlue	число	Параметр предмета 1	Значение параметра	1
obj.val2	obj.val2	DarkBlue	число	Параметр предмета 2	Значение параметра	1
obj.val3	obj.val3	DarkBlue	число	Параметр предмета 3	Значение параметра	1
obj.carried_by	obj.carried_by	DarkBlue	UID	Кто несет	Персонаж или nil	1
obj.worn_by	obj.worn_by	DarkBlue	UID	На ком одет	Персонаж или nil	1
obj.g	obj.g	DarkBlue	строка	о//а/и	Суффикс	1
obj.u	obj.u	DarkBlue	строка	ось/ся/ась/ись	Суффикс	1
obj.w	obj.w	DarkBlue	строка	ое/ый/ая/ые	Суффикс	1
obj.q	obj.q	DarkBlue	строка	ло//ла/ли	Суффикс	1
obj.y	obj.y	DarkBlue	строка	ло//ла/ли	Суффикс	1
obj.a	obj.a	DarkBlue	строка	о//а/ы	Суффикс	1
obj.count	obj.count	DarkBlue	число	Количество предметов в мире (в игре в текущий момент и на ренте)	Количество	1
obj.sex	obj.sex	DarkBlue	число	Род предмета	0 - средний,1 - мужской,2 - женский,3 - мн. число	1
obj.room	obj.room	DarkBlue	число	Комната, в которой находится предмет, или тот на ком он одет.	VNUM комнаты	1
obj.all	obj.all	DarkBlue	список из UID	все в комнате	Список персонажей	1
obj.char	obj.char	DarkBlue	список из UID	все PC и чармисы	Список персонажей	1
obj.pc	obj.pc	DarkBlue	список из UID	все PC	Список персонажей	1
obj.npc	obj.npc	DarkBlue	список из UID	все NPC (мобы не чармисы)	Список персонажей	1
room.name	room.name	DarkBlue	строка	Название комнаты	Название	1
room.north	room.north	DarkBlue	строка	Проверка выхода на север	north ? есть выход, иначе nil	1
room.east	room.east	DarkBlue	строка	Проверка выхода на восток	east ? есть выход, иначе nil	1
room.south	room.south	DarkBlue	строка	Проверка выхода на юг	south ? есть выход, иначе nil	1
room.west	room.west	DarkBlue	строка	Проверка выхода на запад	west ? есть выход, иначе nil	1
room.up	room.up	DarkBlue	строка	Проверка выхода вверх	up ? есть выход, иначе nil	1
room.down	room.down	DarkBlue	строка	Проверка выхода вниз	down ? есть выход, иначе nil	1
room.vnum	room.vnum	DarkBlue	число	vnum комнаты	VNUM	1
room.id	room.id	DarkBlue	число	Получение численного значения UID комнаты room.	Численное значение UID	1
room.people	room.people	DarkBlue	UID	Первый персонаж в комнате room.	Персонаж или nil	1
actor	actor	DarkBlue	Тип не указан	Actor 	Результат не указан	1
actor.iname	actor.iname	DarkBlue	Тип не указан	Имя (именительный падеж) 	Результат не указан	1
actor.rname	actor.rname	DarkBlue	Тип не указан	Имя (родительный падеж) 	Результат не указан	1
actor.dname	actor.dname	DarkBlue	Тип не указан	Имя (дательный падеж) 	Результат не указан	1
actor.vname	actor.vname	DarkBlue	Тип не указан	Имя (винительный падеж) 	Результат не указан	1
actor.tname	actor.tname	DarkBlue	Тип не указан	Имя (творительный падеж) 	Результат не указан	1
actor.pname	actor.pname	DarkBlue	Тип не указан	Имя (предложный падеж) 	Результат не указан	1
actor.name	actor.name	DarkBlue	Тип не указан	Имя 	Результат не указан	1
actor.id	actor.id	DarkBlue	Тип не указан	Получение численного значения UID персонажа 	Результат не указан	1
actor.level	actor.level	DarkBlue	Тип не указан	Уровень персонажа 	Результат не указан	1
actor.hitp	actor.hitp	DarkBlue	Тип не указан	Получение количества hp 	Результат не указан	1
actor.hitp(num)	actor.hitp(num)	DarkBlue	Тип не указан	Изменение hp. Формат num: num – установить значение в num, +num – увеличить значение на num, -num – уменьшить значение на num 	Результат не указан	1
actor.maxhitp	actor.maxhitp	DarkBlue	Тип не указан	Получение максимального количества hp 	Результат не указан	1
actor.mana	actor.mana	DarkBlue	Тип не указан	Получение количества манны 	Результат не указан	1
actor.mana(num)	actor.mana(num)	DarkBlue	Тип не указан	Изменения маны. Формат num: num – установить значение в num, +num – увеличить значение на num, -num – уменьшить значение на num 	Результат не указан	1
actor.maxmana	actor.maxmana	DarkBlue	Тип не указан	Получение максимального количества манны 	Результат не указан	1
actor.move	actor.move	DarkBlue	Тип не указан	Получение количества энергии 	Результат не указан	1
actor.move(num)	actor.move(num)	DarkBlue	Тип не указан	Изменение энергии. Формат num: num – установить значение в num, +num – увеличить значение на num, -num – уменьшить значение на num 	Результат не указан	1
actor.maxmove	actor.maxmove	DarkBlue	Тип не указан	Получение максимального количества энергии 	Результат не указан	1
actor.align	actor.align	DarkBlue	Тип не указан	GET_ALIGNMENT 	Результат не указан	1
actor.religion	actor.religion	DarkBlue	Тип не указан	Религия персонажа 	Результат не указан	1
actor.gold	actor.gold	DarkBlue	Тип не указан	Получение количества денег 	Результат не указан	1
actor.gold(num)	actor.gold(num)	DarkBlue	Тип не указан	Установка количества денег Формат num: num – установить значение в num, +num – увеличить значение на num, -num – уменьшить значение на num 	Результат не указан	1
actor.bank	actor.bank	DarkBlue	Тип не указан	Получение количества денег в банке 	Результат не указан	1
actor.bank(num)	actor.bank(num)	DarkBlue	Тип не указан	Установка количества денег в банке 	Результат не указан	1
actor.exp	actor.exp	DarkBlue	Тип не указан	Получение опыта 	Результат не указан	1
actor.exp(num)	actor.exp(num)	DarkBlue	Тип не указан	Изменение опыта. Формат num: 	Результат не указан	1
actor.sex	actor.sex	DarkBlue	Тип не указан	Пол персонажа 	Результат не указан	1
actor.clan	actor.clan	DarkBlue	Тип не указан	Название клана (в нижнем регистре) 	Результат не указан	1
actor.clanrank	actor.clanrank	DarkBlue	Тип не указан	Положение в клане 	Результат не указан	1
actor.g	actor.g	DarkBlue	Тип не указан	о//а/и 	Результат не указан	1
actor.u	actor.u	DarkBlue	Тип не указан	ось/ся/ась/ись 	Результат не указан	1
actor.w	actor.w	DarkBlue	Тип не указан	ое/ый/ая/ые 	Результат не указан	1
actor.q	actor.q	DarkBlue	Тип не указан	ло//ла/ли 	Результат не указан	1
actor.y	actor.y	DarkBlue	Тип не указан	ло/ел/ла/ли 	Результат не указан	1
actor.a	actor.a	DarkBlue	Тип не указан	о//а/ы 	Результат не указан	1
actor.weight	actor.weight	DarkBlue	Тип не указан	Вес персонажа 	Результат не указан	1
actor.canbeseen	actor.canbeseen	DarkBlue	Тип не указан	Проверка видит ли персонаж self, персонаж 	Результат не указан	1
actor.class	actor.class	DarkBlue	Тип не указан	Класс персонажа 	Результат не указан	1
actor.race	actor.race	DarkBlue	Тип не указан	Раса персонажа 	Результат не указан	1
actor.fighting	actor.fighting	DarkBlue	Тип не указан	Получение противника в бою 	Результат не указан	1
actor.is_killer	actor.is_killer [(on|off)]	DarkBlue	Тип не указан	Управление флагом ДУШЕГУБ.on – устанавливает флаг off – сбрасывает флаг 	Результат не указан	1
actor.is_thief 	actor.is_thief [(on|off)]	DarkBlue	Тип не указан	Работа с флагом PLR_THIEF 	Результат не указан	1
actor.rentable	actor.rentable	DarkBlue	Тип не указан	Проверка на боевые действия 	Результат не указан	1
actor.riding	actor.riding	DarkBlue	Тип не указан	Определение лошади 	Результат не указан	1
actor.ridden_by	actor.ridden_by	DarkBlue	Тип не указан	Определение наездника 	Результат не указан	1
actor.vnum	actor.vnum	DarkBlue	Тип не указан	vnum моба. –1 для PC 	Результат не указан	1
actor.str	actor.str	DarkBlue	Тип не указан	Врожденная сила персонажа 	Результат не указан	1
actor.str(num)	actor.str(num)	DarkBlue	Тип не указан	Изменение врожденной силы 	Результат не указан	1
actor.stradd	actor.stradd	DarkBlue	Тип не указан	Добавочная сила персонажа 	Результат не указан	1
actor.int	actor.int	DarkBlue	Тип не указан	Врожденный ум персонажа 	Результат не указан	1
actor.int(num)	actor.int(num)	DarkBlue	Тип не указан	Изменение врожденного ума 	Результат не указан	1
actor.intadd	actor.intadd	DarkBlue	Тип не указан	Добавочный ум персонажа 	Результат не указан	1
actor.wis	actor.wis	DarkBlue	Тип не указан	Врожденная мудрость персонажа 	Результат не указан	1
actor.wis(num)	actor.wis(num)	DarkBlue	Тип не указан	Изменение врожденной мудрости 	Результат не указан	1
actor.wisadd	actor.wisadd	DarkBlue	Тип не указан	Добавочная мудрость персонажа 	Результат не указан	1
actor.dex	actor.dex	DarkBlue	Тип не указан	Врожденная ловкость персонажа 	Результат не указан	1
actor.dex(num)	actor.dex(num)	DarkBlue	Тип не указан	Изменение врожденной ловкости 	Результат не указан	1
actor.dexadd	actor.dexadd	DarkBlue	Тип не указан	Добавочная ловкость персонажа 	Результат не указан	1
actor.con	actor.con	DarkBlue	Тип не указан	Врожденное тело персонажа 	Результат не указан	1
actor.con(num)	actor.con(num)	DarkBlue	Тип не указан	Изменение врожденного тела 	Результат не указан	1
actor.conadd	actor.conadd	DarkBlue	Тип не указан	Добавочное тело персонажа 	Результат не указан	1
actor.cha	actor.cha	DarkBlue	Тип не указан	Врожденное обаяние персонажа 	Результат не указан	1
actor.cha(num)	actor.cha(num)	DarkBlue	Тип не указан	Изменение врожденного обаяния 	Результат не указан	1
actor.chaadd	actor.chaadd	DarkBlue	Тип не указан	Добавочное обаяние персонажа 	Результат не указан	1
actor.size	actor.size	DarkBlue	Тип не указан	Врожденный размер персонажа 	Результат не указан	1
actor.size(num)	actor.size(num)	DarkBlue	Тип не указан	Изменение врожденного размера 	Результат не указан	1
actor.sizeadd	actor.sizeadd	DarkBlue	Тип не указан	Добавочный размер персонажа 	Результат не указан	1
actor.room	actor.room	DarkBlue	Тип не указан	Получение комнаты, в которой находится персонаж 	Результат не указан	1
actor.realroom	actor.realroom	DarkBlue	Тип не указан	VNUM комнаты, в которой находится персонаж 	Результат не указан	1
actor.loadroom	actor.loadroom	DarkBlue	Тип не указан	Получение загрузочной комнаты 	Результат не указан	1
actor.loadroom(vnum)	actor.loadroom(vnum)	DarkBlue	Тип не указан	Установка загрузочной комнаты 	Результат не указан	1
actor.skill(str)	actor.skill(str)	DarkBlue	Тип не указан	Уровень умения str у персонажа.Str – название умения 	Результат не указан	1
actor.spellcount(str)	actor.spellcount(str)	DarkBlue	Тип не указан	Количество у персонажа выученных заклинаний str.Str – имя заклинания 	Результат не указан	1
actor.spelltype(str)	actor.spelltype(str)	DarkBlue	Тип не указан	Тип запоминания заклинания str у персонажа. Флаги из массива SplKnw. Str – имя заклинания 	Результат не указан	1
actor.quested(num)	actor.quested(num)	DarkBlue	Тип не указан	Проверка на то, выполнял ли персонаж квест номер num. 	Результат не указан	1
actor.setquest(num)	actor.setquest(num)	DarkBlue	Тип не указан	Установка признака выполнения квеста номер num 	Результат не указан	1
actor.eq(pos)	actor.eq(pos)	DarkBlue	Тип не указан	Получение предмета экипировки.pos – позиция (текст или номер позиции) 	Результат не указан	1
actor.haveobj(obj)	actor.haveobj(obj)	DarkBlue	Тип не указан	Несет ли персонаж предмет obj? obj может быть vnum или именем предмета. 	Результат не указан	1
actor.varexists(name)	actor.varexists(name)	DarkBlue	Тип не указан	Проверяет у сценария объекта char наличие глобальной переменной name. 	Результат не указан	1
actor.position	actor.position	DarkBlue	Тип не указан	Возвращает положение персонажа 	Результат не указан	1
actor.position(pos)	actor.position(pos)	DarkBlue	Тип не указан	Устанавливает позицию персонажа. Не действует на богов и т.д. 	Результат не указан	1
actor.wait	actor.wait	DarkBlue	Тип не указан	Возвращает лаг персонажа в секундах. 	Результат не указан	1
actor.wait(pause)	actor.wait(pause)	DarkBlue	Тип не указан	Устанавливает лаг персонажа в пульсах.Не действует на богов и т.д. 	Результат не указан	1
actor.affect(name)	actor.affect(name)	DarkBlue	Тип не указан	Проверяет наличие на персонаже аффекта name. 	Результат не указан	1
actor.leader	actor.leader	DarkBlue	Тип не указан	Возвращает лидера для 	Результат не указан	1
actor.people	actor.people	DarkBlue	Тип не указан	Первый персонаж в комнате с 	Результат не указан	1
actor.next_in_room	actor.next_in_room	DarkBlue	Тип не указан	Следующий за char персонаж в комнате 	Результат не указан	1
actor.all 	actor.all 	DarkBlue	Тип не указан	Список персонажей в комнате:all - все в комнате  	Результат не указан	1
actor.char 	actor.char 	DarkBlue	Тип не указан	Список персонажей в комнате:char - все PC и чармисы 	Результат не указан	1
actor.pc 	actor.pc 	DarkBlue	Тип не указан	Список персонажей в комнате:pc - все PC  	Результат не указан	1
actor.npc	actor.npc	DarkBlue	Тип не указан	Список персонажей в комнате:  npc – все NPC (мобы не чармисы) 	Результат не указан	1
actor.global(name)	actor.global(name)	DarkBlue	Тип не указан	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария 	Результат не указан	1
actor.var	actor.var	DarkBlue	Тип не указан	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария self. 	Результат не указан	1
self.iname	self.iname	DarkBlue	Тип не указан	Имя (именительный падеж) 	Результат не указан	1
self.rname	self.rname	DarkBlue	Тип не указан	Имя (родительный падеж) 	Результат не указан	1
self.dname	self.dname	DarkBlue	Тип не указан	Имя (дательный падеж) 	Результат не указан	1
self.vname	self.vname	DarkBlue	Тип не указан	Имя (винительный падеж) 	Результат не указан	1
self.tname	self.tname	DarkBlue	Тип не указан	Имя (творительный падеж) 	Результат не указан	1
self.pname	self.pname	DarkBlue	Тип не указан	Имя (предложный падеж) 	Результат не указан	1
self.name	self.name	DarkBlue	Тип не указан	Короткое описание (если есть), иначе имя 	Результат не указан	1
self.id	self.id	DarkBlue	Тип не указан	Получение численного значения UID персонажа self. 	Результат не указан	1
self.alias	self.alias	DarkBlue	Тип не указан	Имя 	Результат не указан	1
self.level	self.level	DarkBlue	Тип не указан	Уровень персонажа 	Результат не указан	1
self.hitp	self.hitp	DarkBlue	Тип не указан	Получение количества hp 	Результат не указан	1
self.hitp(num)	self.hitp(num)	DarkBlue	Тип не указан	Изменение hp. Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num 	Результат не указан	1
self.maxhitp	self.maxhitp	DarkBlue	Тип не указан	Получение максимального количества hp 	Результат не указан	1
self.mana	self.mana	DarkBlue	Тип не указан	Получение количества манны 	Результат не указан	1
self.mana(num)	self.mana(num)	DarkBlue	Тип не указан	Изменения маны. Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num 	Результат не указан	1
self.maxmana	self.maxmana	DarkBlue	Тип не указан	Получение максимального количества манны 	Результат не указан	1
self.move	self.move	DarkBlue	Тип не указан	Получение количества энергии 	Результат не указан	1
self.move(num)	self.move(num)	DarkBlue	Тип не указан	Изменение энергии. Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num 	Результат не указан	1
self.maxmove	self.maxmove	DarkBlue	Тип не указан	Получение максимального количества энергии 	Результат не указан	1
self.align	self.align	DarkBlue	Тип не указан	GET_ALIGNMENT 	Результат не указан	1
self.religion	self.religion	DarkBlue	Тип не указан	Религия персонажа 	Результат не указан	1
self.gold	self.gold	DarkBlue	Тип не указан	Получение количества денег 	Результат не указан	1
self.gold(num)	self.gold(num)	DarkBlue	Тип не указан	Установка количества денег Формат num: num - установить значение в num, +num - увеличить значение на num, -num - уменьшить значение на num 	Результат не указан	1
self.bank	self.bank	DarkBlue	Тип не указан	Получение количества денег в банке 	Результат не указан	1
self.bank(num)	self.bank(num)	DarkBlue	Тип не указан	Установка количества денег в банке 	Результат не указан	1
self.exp	self.exp	DarkBlue	Тип не указан	Получение опыта 	Результат не указан	1
self.exp(num)	self.exp(num)	DarkBlue	Тип не указан	Изменение опыта. Формат num: 	Результат не указан	1
self.sex	self.sex	DarkBlue	Тип не указан	Пол персонажа 	Результат не указан	1
self.clan	self.clan	DarkBlue	Тип не указан	Название клана (в нижнем регистре) 	Результат не указан	1
self.clanrank	self.clanrank	DarkBlue	Тип не указан	Положение в клане 	Результат не указан	1
self.g	self.g	DarkBlue	Тип не указан	о//а/и 	Результат не указан	1
self.u	self.u	DarkBlue	Тип не указан	ось/ся/ась/ись 	Результат не указан	1
self.w	self.w	DarkBlue	Тип не указан	ое/ый/ая/ые 	Результат не указан	1
self.q	self.q	DarkBlue	Тип не указан	ло//ла/ли 	Результат не указан	1
self.y	self.y	DarkBlue	Тип не указан	ло/ел/ла/ли 	Результат не указан	1
self.a	self.a	DarkBlue	Тип не указан	о//а/ы 	Результат не указан	1
self.weight	self.weight	DarkBlue	Тип не указан	Вес персонажа 	Результат не указан	1
self.canbeseen	self.canbeseen	DarkBlue	Тип не указан	Проверка видит ли персонаж self, персонаж self.	Результат не указан	1
self.class	self.class	DarkBlue	Тип не указан	Класс персонажа 	Результат не указан	1
self.race	self.race	DarkBlue	Тип не указан	Раса персонажа 	Результат не указан	1
self.fighting	self.fighting	DarkBlue	Тип не указан	Получение противника в бою 	Результат не указан	1
self.is_killer	self.is_killer [(on|off)]	DarkBlue	Тип не указан	Управление флагом ДУШЕГУБ.on - устанавливает флаг off - сбрасывает флаг 	Результат не указан	1
self.is_thief	self.is_thief [(on|off)]	DarkBlue	Тип не указан	Работа с флагом PLR_THIEF 	Результат не указан	1
self.rentable	self.rentable	DarkBlue	Тип не указан	Проверка на боевые действия 	Результат не указан	1
self.riding	self.riding	DarkBlue	Тип не указан	Определение лошади 	Результат не указан	1
self.ridden_by	self.ridden_by	DarkBlue	Тип не указан	Определение наездника 	Результат не указан	1
self.vnum	self.vnum	DarkBlue	Тип не указан	vnum моба. -1 для PC 	Результат не указан	1
self.str	self.str	DarkBlue	Тип не указан	Врожденная сила персонажа 	Результат не указан	1
self.str(num)	self.str(num)	DarkBlue	Тип не указан	Изменение врожденной силы 	Результат не указан	1
self.stradd	self.stradd	DarkBlue	Тип не указан	Добавочная сила персонажа 	Результат не указан	1
self.int	self.int	DarkBlue	Тип не указан	Врожденный ум персонажа 	Результат не указан	1
self.int(num)	self.int(num)	DarkBlue	Тип не указан	Изменение врожденного ума 	Результат не указан	1
self.intadd	self.intadd	DarkBlue	Тип не указан	Добавочный ум персонажа 	Результат не указан	1
self.wis	self.wis	DarkBlue	Тип не указан	Врожденная мудрость персонажа 	Результат не указан	1
self.wis(num)	self.wis(num)	DarkBlue	Тип не указан	Изменение врожденной мудрости 	Результат не указан	1
self.wisadd	self.wisadd	DarkBlue	Тип не указан	Добавочная мудрость персонажа 	Результат не указан	1
self.dex	self.dex	DarkBlue	Тип не указан	Врожденная ловкость персонажа 	Результат не указан	1
self.dex(num)	self.dex(num)	DarkBlue	Тип не указан	Изменение врожденной ловкости 	Результат не указан	1
self.dexadd	self.dexadd	DarkBlue	Тип не указан	Добавочная ловкость персонажа 	Результат не указан	1
self.con	self.con	DarkBlue	Тип не указан	Врожденное тело персонажа 	Результат не указан	1
self.con(num)	self.con(num)	DarkBlue	Тип не указан	Изменение врожденного тела 	Результат не указан	1
self.conadd	self.conadd	DarkBlue	Тип не указан	Добавочное тело персонажа 	Результат не указан	1
self.cha	self.cha	DarkBlue	Тип не указан	Врожденное обаяние персонажа 	Результат не указан	1
self.cha(num)	self.cha(num)	DarkBlue	Тип не указан	Изменение врожденного обаяния 	Результат не указан	1
self.chaadd	self.chaadd	DarkBlue	Тип не указан	Добавочное обаяние персонажа 	Результат не указан	1
self.size	self.size	DarkBlue	Тип не указан	Врожденный размер персонажа 	Результат не указан	1
self.size(num)	self.size(num)	DarkBlue	Тип не указан	Изменение врожденного размера 	Результат не указан	1
self.sizeadd	self.sizeadd	DarkBlue	Тип не указан	Добавочный размер персонажа 	Результат не указан	1
self.room	self.room	DarkBlue	Тип не указан	Получение комнаты, в которой находится персонаж 	Результат не указан	1
self.realroom	self.realroom	DarkBlue	Тип не указан	VNUM комнаты, в которой находится персонаж 	Результат не указан	1
self.loadroom	self.loadroom	DarkBlue	Тип не указан	Получение загрузочной комнаты 	Результат не указан	1
self.loadroom(vnum)	self.loadroom(vnum)	DarkBlue	Тип не указан	Установка загрузочной комнаты 	Результат не указан	1
self.skill(str)	self.skill(str)	DarkBlue	Тип не указан	Уровень умения str у персонажа.Str - название умения 	Результат не указан	1
self.spellcount(str)	self.spellcount(str)	DarkBlue	Тип не указан	Количество у персонажа выученных заклинаний str.Str - имя заклинания 	Результат не указан	1
self.spelltype(str)	self.spelltype(str)	DarkBlue	Тип не указан	Тип запоминания заклинания str у персонажа. Флаги из массива SplKnw. Str - имя заклинания 	Результат не указан	1
self.quested(num)	self.quested(num)	DarkBlue	Тип не указан	Проверка на то, выполнял ли персонаж квест номер num. 	Результат не указан	1
self.setquest(num)	self.setquest(num)	DarkBlue	Тип не указан	Установка признака выполнения квеста номер num 	Результат не указан	1
self.eq(pos)	self.eq(pos)	DarkBlue	Тип не указан	Получение предмета экипировки.pos - позиция (текст или номер позиции) 	Результат не указан	1
self.haveobj(obj)	self.haveobj(obj)	DarkBlue	Тип не указан	Несет ли персонаж предмет obj? obj может быть vnum или именем предмета. 	Результат не указан	1
self.varexists(name)	self.varexists(name)	DarkBlue	Тип не указан	Проверяет у сценария объекта char наличие глобальной переменной name. 	Результат не указан	1
self.position	self.position	DarkBlue	Тип не указан	Возвращает положение персонажа 	Результат не указан	1
self.position(pos)	self.position(pos)	DarkBlue	Тип не указан	Устанавливает позицию персонажа. Не действует на богов и т.д. 	Результат не указан	1
self.wait	self.wait	DarkBlue	Тип не указан	Возвращает лаг персонажа в секундах. 	Результат не указан	1
self.wait(pause)	self.wait(pause)	DarkBlue	Тип не указан	Устанавливает лаг персонажа в пульсах.Не действует на богов и т.д. 	Результат не указан	1
self.affect(name)	self.affect(name)	DarkBlue	Тип не указан	Проверяет наличие на персонаже аффекта name. 	Результат не указан	1
self.leader	self.leader	DarkBlue	Тип не указан	Возвращает лидера для self. 	Результат не указан	1
self.people	self.people	DarkBlue	Тип не указан	Первый персонаж в комнате с self. 	Результат не указан	1
self.next_in_room	self.next_in_room	DarkBlue	Тип не указан	Следующий за char персонаж в комнате 	Результат не указан	1
self.all 	self.all 	DarkBlue	Тип не указан	Список персонажей в комнате:all - все в комнате  	Результат не указан	1
self.char 	self.char 	DarkBlue	Тип не указан	Список персонажей в комнате:char - все PC и чармисы 	Результат не указан	1
self.pc 	self.pc 	DarkBlue	Тип не указан	Список персонажей в комнате:pc - все PC  	Результат не указан	1
self.npc	self.npc	DarkBlue	Тип не указан	Список персонажей в комнате:  npc - все NPC (мобы не чармисы) 	Результат не указан	1
self.global(name)	self.global(name)	DarkBlue	Тип не указан	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария self.	Результат не указан	1
self.var	self.var	DarkBlue	Тип не указан	Поиск и замена глобальной переменной другого сценария. При поиске переменной используется текущий контекст сценария self. 	Результат не указан	1

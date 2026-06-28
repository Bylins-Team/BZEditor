* Правило формирования строки флага: 1[Tab]2[Tab]3[Tab]4[Tab]5
* 1 - флаг
* 2 - Описание флага
* 3 - Цвет текста
* 4 - Цвет фона
* 5 - Приоритетность (0 - самый высокий приоритет, 100 - по умолчанию) 
* если у комнаты несколько флагов то комната на карте будет отображена цветом в соответствии в приоритетом
G	Ограничения
a0	Темная	Default	Default	100
l1	Всегда светло	Default	Default	100
c0	Не для мобов (МОБы не могут зайти сюда)	Default	Default	100
e0	Мирная (запрещены агрессивные команды)	Black	LightGreen	3
j1	Нельзя атаковать	Default	LightGreen	100
f0	Глухая (крики, tell'ы и прочее нельзя здесь услышать)	Default	Default	100
g0	Не выследить ()track не сможет найти путь через эту комнату)	Default	Default	100
h0	Нет магии (любая попытка колдовать здесь провалится)	Default	Default	100
*k0	Только LVL_GOD (богам) разрешено сюда входить	Default	Default	100
d0	Комната внутри помещения. (нет дождя и т.д.)	Default	Default	100
c1	Скакун сюда не проедет	Default	Default	100
d1	На комнату не влияют погодные условия	Default	Default	100
a2	Передача вещей запрещена	Default	Default	100
G	Опасность
b0	ДТ (игрок умирает, не теряет xp)	White	Black	0
e1	Медленное ДТ	White	DarkRed	2
f1	Провалился под лед	White	DarkBlue	1
G	Особенности
y0	Жертвенник	Default	Default	100
z0	Молельня	Default	Default	100
D0	Комната является частью арены	Default	Default	100
h1	Слышно арену	Default	Default	100
i1	Транслируем арену	Default	Default	100
k1	Квестовая	Default	Default	100
G	Кланзамки
l0	Замок (комната часть замка)	Default	Default	100
*m0	(R) House needs saving	Default	Default	100
n0	Вход в замок	Default	Default	100
*Aa	(R) Modifyable/!compress	Default	Default	100
*p0	(R) breath-first srch mrk	Default	Default	100
G	Благоприятствие
q0	Для магов (кол,вол,куд,черн)	Default	Default	100
r0	Для лекарей	Default	Default	100
s0	Для воров	Default	Default	100
t0	Для воинов	Default	Default	100
u0	Для наемников	Default	Default	100
v0	Для дружинников	Default	Default	100
w0	Для витязей	Default	Default	100
x0	Для охотников	Default	Default	100
A0	Для кузнецов (хинт: кузница для перековки)	Default	Default	100
B0	Для купцов	Default	Default	100
C0	Для волхвов	Default	Default	100
b2	Для русичей	Default	Default	100
c2	Для викингов	Default	Default	100
d2	Для степняков	Default	Default	100
G	Транспорт
a1	Не призвать	Default	Default	100
b1	Телепортация и рекол из комнаты невозможны	Default	Default	100
i0	Однопроходная (только один человек может быть здесь одновременно)	Default	Default	100
j0	Нельзя телепортироваться в эту комнату	Default	Default	100
g1	Не переместиться	Default	Default	100
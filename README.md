# UpToDown Shooter
**Пошаговый шутер с видом сверху**

Игрок при запуске сразу попадает на готовую карту и должен отстреливаться от прибывающих врагов, и набрать как модно большее число очков.

## Структура мира
- Весь уровень расчерчен на квадраты, в одном из них будет игрок, и по которым будут двигаться враги

**Tiles**
- Пустота
- Игрок
- Враг
- Стена
- Пуля

## Геймплей
**Действия**
- Передвинуться в соседнюю клетку
- Выстрелить в одном из направлений
**После любого из действий начитнается черед врагов, занимающих одну клетку, они могут:**
- Передвинуться к игроку по заранее расчитанному пути
- Умереть от пули
**Пуля при запуске начинает движение и останавливается только при столкновении с каким-нибудь объектом**
**Пуля так же занимает одну клетку**

**Основной задачей игрока является набираниие как можно большего числа очков за убийство врагов**

**Сложность повышается разными уровнями и повышением частоты появления врагов**

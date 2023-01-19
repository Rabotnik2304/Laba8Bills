# Laba8Bills
Задание 2. Выписка по счету

Дан файл с выпиской по банковскому счету, который имеет следующий формат. На первой строке в файле задана начальная сумма на счете, затем на каждой строке описана операция по изменению состояния счета. Через символ "|" перечислены дата и время операции, сумма и тип операции. in - зачисление денег на счет out - вывод денег со счета revert - отменяет последнюю операцию, какой бы она ни была
Во-первых, требуется проверить правильность данных в файле. Например, в этом файле будет ошибка

1000
2021-06-01 12:00 | 500 | in
2021-06-01 12:05 | 500 | out
2021-06-01 12:10 | 1000 | out
2021-06-02 10:00 | 500 | out  


т.к. расход превысил остаток по карте. Пример корректного файла:

1000
2021-06-01 12:00 | 500 | in
2021-06-01 12:05 | 500 | out
2021-06-01 12:10 | 1000 | out
2021-06-01 12:00 | 1200 | in
2021-06-02 10:00 | 500 | out
2021-06-01 12:05 | revert


Затем пользовать вводит дату и время. Вывести кол-во средств на счету карты на этот момент времени. Если пользователь не вводит никакую дату вывести итоговый остаток средств.

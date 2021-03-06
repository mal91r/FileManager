# FileManager
Необходимо разработать консольное приложение – «Файловый менеджер».
Допустима как реализация приложения-эмулятора командной строки, так и
приложения с набором нумерованных текстовых меню. При выборе первого 
варианта обязательно требуется реализация команды “help” с подробным 
описанием возможных действий. Не обязательно реализовывать 11 различных 
команд, одна команда может выполнять несколько операций, в зависимости от 
передаваемых параметров.
Алгоритм работы приложения: 
В начале работы программы пользователь выбирает операцию, которую 
хочет выполнить. Среди доступных операций должны присутствовать:
1. просмотр списка дисков компьютера и выбор диска;
2. переход в другую директорию (выбор папки);
3. просмотр списка файлов в директории;
4. вывод содержимого текстового файла в консоль в кодировке UTF-8;
5. вывод содержимого текстового файла в консоль в выбранной 
пользователем кодировке (предоставляется не менее трех вариантов);
6. копирование файла;
7. перемещение файла в выбранную пользователем директорию;
8. удаление файла;
9. создание простого текстового файла в кодировке UTF-8;
10.создание простого текстового файла в выбранной пользователем 
кодировке (предоставляется не менее трех вариантов);
11.конкатенация содержимого двух или более текстовых файлов и вывод 
результата в консоль в кодировке UTF-8.

Дополнительные операции(дополнительный функционал):
1. Выполнить вывод всех файлов в текущей директории по заданной маске 
(маска задаётся в виде регулярного выражения: например, ввод 
«*.docx?» приведёт к выводу списка всех файлов Microsoft Word в 
директории, как старой, так и новой версии)
2. Выполнить вывод всех файлов в текущей директории и всех её 
поддиректориях по заданной маске.
3. Скопировать все файлы из директории и всех её поддиректорий по маске
в другую директорию, причём, если директория, в которую происходит 
копирование, не существует – она создаётся. Также необходимо
предусмотреть возможность двух вариантов поведения при наличии
файла с таким же названием в директории, в которую копируются 
файлы: замена файла на новый или оставление старого файла без 
изменений.
4. Сравнить и вывести различия между двумя текстовыми файлами по 
аналогии с функцией diff. Автору этого пункта больше всего 
импонирует вывод вида «diff -u original», но можно также представить 
любой другой из описанных в статье1
.
5. Дополнить часть введенного пути или имени файла до полного имени 
каталога или файла по нажатию клавиши Tab. Если возможный вариант 
дополнения один, путь дополняется полностью (например, в директории 
есть один файл «orange.txt» и было введено «or», по нажатию кнопки Tab
введенное имя должно быть дополнено до «orange.txt» или если в 
директории есть поддиректория fruits, то fr дополняется до «fruits/»).
Если же вариантов дополнения несколько, путь дополняется до общей 
части (в директории есть файлы «apple.txt» и «apple.pdf» и было введено 
«ap», по нажатию кнопки введенное имя должно быть дополнено до 
«apple.»).
После выбора операции программа должна выполнять запрошенную
операцию с файловой системой компьютера и сообщать о результате.

// Подключаем стандартную библиотеку System.
// Она нужна для работы с консолью: Console.WriteLine, Console.ReadKey и так далее.
using System;

// Выводим название практической работы, паттерна и выбранного варианта.
Console.WriteLine("Практическая 7. Prototype. Вариант 1 - журнал документов.");
Console.WriteLine();

// Создаем оригинальный TXT-документ.
// Это первый прототип, который потом можно будет клонировать.
Document txt = new TxtDocument("Заметки", "Иван", 3, "UTF-8");

// Создаем оригинальный DOC-документ.
Document doc = new DocDocument("Отчет", "Мария", 12, true);

// Создаем оригинальный PDF-документ.
Document pdf = new PdfDocument("Инструкция", "Петр", 20, false);

// Клонируем TXT-документ.
// Метод Clone() создает копию существующего объекта.
Document txtCopy = txt.Clone();

// Меняем название только у копии, чтобы показать, что это отдельный объект.
txtCopy.Title = "Заметки - копия";

// Клонируем DOC-документ.
Document docCopy = doc.Clone();
docCopy.Title = "Отчет - копия";

// Клонируем PDF-документ.
Document pdfCopy = pdf.Clone();
pdfCopy.Title = "Инструкция - копия";

// Выводим оригиналы и копии на экран.
PrintDocumentPair(txt, txtCopy);
PrintDocumentPair(doc, docCopy);
PrintDocumentPair(pdf, pdfCopy);

// Оставляем консоль открытой до нажатия клавиши.
Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();

// Метод для вывода пары документов: оригинал и его клон.
static void PrintDocumentPair(Document original, Document clone)
{
    Console.WriteLine("Оригинал: " + original);
    Console.WriteLine("Клон:     " + clone);
    Console.WriteLine();
}

// Абстрактный класс Document - это общий прототип для всех документов.
// Абстрактный значит, что напрямую Document создавать нельзя.
// Можно создавать только конкретные документы: TxtDocument, DocDocument, PdfDocument.
abstract class Document
{
    // Название документа.
    public string Title;

    // Автор документа.
    public string Author;

    // Количество страниц.
    public int Pages;

    // Конструктор заполняет общие поля документа при создании объекта.
    public Document(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }

    // Абстрактный метод Clone().
    // Каждый конкретный документ обязан сам описать, как он копируется.
    public abstract Document Clone();
}

// TxtDocument - конкретный тип документа TXT.
// Он наследуется от Document, поэтому имеет Title, Author и Pages.
class TxtDocument : Document
{
    // Дополнительное поле именно для TXT: кодировка файла.
    public string Encoding;

    // Конструктор TXT-документа.
    // Через base(...) передаем общие данные в родительский класс Document.
    public TxtDocument(string title, string author, int pages, string encoding)
        : base(title, author, pages)
    {
        Encoding = encoding;
    }

    // Метод Clone создает копию TXT-документа.
    // MemberwiseClone() копирует текущий объект.
    public override Document Clone()
    {
        return (TxtDocument)MemberwiseClone();
    }

    // Метод ToString отвечает за красивый вывод TXT-документа в консоль.
    public override string ToString()
    {
        return "TXT: " + Title + ", автор: " + Author + ", страниц: " + Pages + ", кодировка: " + Encoding;
    }
}

// DocDocument - конкретный тип документа DOC.
class DocDocument : Document
{
    // Поле показывает, есть ли в документе форматирование.
    public bool HasFormatting;

    // Конструктор DOC-документа.
    public DocDocument(string title, string author, int pages, bool hasFormatting)
        : base(title, author, pages)
    {
        HasFormatting = hasFormatting;
    }

    // Создаем копию DOC-документа.
    public override Document Clone()
    {
        return (DocDocument)MemberwiseClone();
    }

    // Красиво выводим информацию о DOC-документе.
    public override string ToString()
    {
        string text = HasFormatting ? "есть форматирование" : "без форматирования";
        return "DOC: " + Title + ", автор: " + Author + ", страниц: " + Pages + ", " + text;
    }
}

// PdfDocument - конкретный тип документа PDF.
class PdfDocument : Document
{
    // Поле показывает, защищен ли PDF-документ.
    public bool IsProtected;

    // Конструктор PDF-документа.
    public PdfDocument(string title, string author, int pages, bool isProtected)
        : base(title, author, pages)
    {
        IsProtected = isProtected;
    }

    // Создаем копию PDF-документа.
    public override Document Clone()
    {
        return (PdfDocument)MemberwiseClone();
    }

    // Красиво выводим информацию о PDF-документе.
    public override string ToString()
    {
        string text = IsProtected ? "защищен" : "не защищен";
        return "PDF: " + Title + ", автор: " + Author + ", страниц: " + Pages + ", " + text;
    }
}

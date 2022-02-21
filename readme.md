
AdtGekid
=========
_This project is in german, since it covers purely data structures, that are necessary for reporting to german authorities._



Entitätsklassen für den onkologischen Basisdatensatz nach ADT/GEKID, wie er im neuen KFRG (Krebsfrüherkennungs- und -registergesetz) Anwendung findet ([siehe hier](http://www.tumorzentren.de/onkol-basisdatensatz.html)).

Derzeitige Version: 2.2.1 (wo MM = without Module Malignant Melanoma)

Das originale XML-Schema-Dokument (Version 2.2.1) des BDT ist in den Testklassen enthalten. 
Die Entitätsklassen enthalten Validierungen (soweit im BDT vorgegeben) für ihre Eigenschaften. Bei Validierungsfehlern wird eine Ausnahme des Typs `ArgumentException` ausgelöst. 
An einigen Stellen wird anstatt von String-Arrays ein eigener Datentyp verwendet (`ValidatedStringList`), der die Validierung der einzelnen Strings ermöglicht.

Ziel ist es vor allem, das Serialisieren und Deserialisieren nach/von XML umzusetzen, wobei Validität zum angegebenen XML-Schema gewährleistet werden soll. Dazu existieren zum Teil eigene Klassen als Pendant zu dem XML Typen, um die richtige Serialisierung zu ermöglichen.
Der Code zum Erzeugen eines XML Files ist dadurch recht einfach:

```cs
    var serializer =  new XmlSerializer(typeof(Root));
    using (var writer = new StreamWriter(outputFileName))
    {
        var settings = new XmlWriterSettings { Indent = true, IndentChars = "  ", Encoding = Encoding.UTF8 });
        using (var xmlWriter = XmlWriter.Create(writer, settings)
       {
            serializer.Serialize(xmlWriter, adtObject);
       }
    }
```

Analog dazu das Deserialisieren
```cs
    var serializer =  new XmlSerializer(typeof(Root));
    using (var reader = new StreamReader(filepath, Encoding.UTF8))
    {
        var obj = serializer.Deserialize(reader) as Root;
    }
```
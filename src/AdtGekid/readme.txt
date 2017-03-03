AdtGekid

This project is in german, since it covers purely data structures, that are necessary for reporting to german authorities.

Entitätsklassen für den onkologischen Basisdatensatz nach ADT/GEKID, wie er im neuen KFRG (Krebsfrüherkennungs- und -registergesetz) Anwendung findet (http://www.tumorzentren.de/onkol-basisdatensatz.html).

version 1.0.5.14 (Maddogtannen)
- Validation of Histologie_Typ.Code according to ADT/GEKID XSD schema

version 1.0.5.13 (Maddogtannen)
- Serialization of "JnuTyp.Unbekannt" enabled

version 1.0.5.12 (Maddogtannen)
- Name of property MeldeanlassSpecified changed to AnlassSpecified due to correct serialization if Anlass = null
- Correct serialization of property Stammdaten.FruehereNamen if null


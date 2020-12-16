AdtGekid

This project is in german, since it covers purely data structures, that are necessary for reporting to german authorities.

Entitätsklassen für den onkologischen Basisdatensatz nach ADT/GEKID, wie er im neuen KFRG (Krebsfrüherkennungs- und -registergesetz) Anwendung findet (http://www.tumorzentren.de/onkol-basisdatensatz.html).

version 2.1.1.1 (MadDogTannen)
- Changes of 1.0.5.19 had been missing

version 2.1.1.0 (MadDogTannen)
- Added ICD-/ OPS-versions missing in 2.0.0
- Added Module for "Prostata"

version 2.0.0.0 (Maddogtannen)
- On the lines of ADT/GEKID Version 2.0.0, many types are enumerations now
- Added Modules for "Allgemein", "Mamma" and "Darm

version 1.0.5.19 (Maddogtannen)
- Verlauf.Datum-Validation: Date musn't be in future

version 1.0.5.18 (Maddogtannen)
- Detailled errors with information of fields validated

version 1.0.5.17 (Maddogtannen)
- Empty container elements like Menge_Frueherer_Name will not be serialized any more

version 1.0.5.16 (Maddogtannen)
- Fixed deserialization of SYST_Substanz
- Corrected allowed value range of ZusatzItem.Wert for Untersuchungsanlass

version 1.0.5.15 (Maddogtannen)
- Switched validation of Patient ID from alphanumeric to max length of 16 only

version 1.0.5.14 (Maddogtannen)
- Validation of Histologie_Typ.Code according to ADT/GEKID XSD schema

version 1.0.5.13 (Maddogtannen)
- Serialization of "JnuTyp.Unbekannt" enabled

version 1.0.5.12 (Maddogtannen)
- Name of property MeldeanlassSpecified changed to AnlassSpecified due to correct serialization if Anlass = null
- Correct serialization of property Stammdaten.FruehereNamen if null

version 1.0.5.12 (Maddogtannen)
- Possiblity to disable validation of KrankenVersichertenNr/KrankenkassenNr with static property Stammdaten.KrankenversichertenNrValidationEnabled/KrankenkassenNrValidationEnabled for deserialization of invalid or empty field
- Validation for Adresse.Hausnummer corrected



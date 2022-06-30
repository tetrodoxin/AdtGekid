using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Xunit;
using System.Collections.ObjectModel;

namespace AdtGekid.Tests
{
    using Module;
    using Module.Prostata;

    public class SerializerTests
    {        
        public const string SchemaFileName = @"ADT_GEKID_v2.2.1.xsd";


        [Fact]    
        public void Serialize()
        {
            string outputFileName = "serializer_output.xml";
            string sampleFileName = "sample.xml";

            serialize(outputFileName);
            var serializedDocument = readDocument(outputFileName);

            Assert.NotNull(serializedDocument);

            // Gegen XSD validieren
            testOnValidationErrors(serializedDocument);

            var sampleDocument = readDocument(sampleFileName);
            Assert.NotNull(sampleDocument);

            // Serialisiertes mit Sample vergleichen
            compareXmlNodes(sampleDocument.DocumentElement, serializedDocument.DocumentElement);
        }

        /// <summary>
        /// Soll testen ob korrekt serialisiert wird
        /// wenn die Enumerationen leer sind,
        /// z.B. sollte bei leerer <see cref="StrahlendosisTyp.Einheit"/> nicht 
        /// <see cref="StrahlendosisEinheit.NotSpecified"/> serialisiert werden
        /// </summary>
        public void SerializeEmptyEnumsTest()
        {
            throw new System.NotImplementedException();
            //string outputFileName = "serializer_output_empty_enums.xml";
            //string sampleFileName = "sample.xml";

            //serialize(outputFileName);
            //var serializedDocument = readDocument(outputFileName);

            //Assert.NotNull(serializedDocument);

            //// Gegen XSD validieren
            //testOnValidationErrors(serializedDocument);

            //var sampleDocument = readDocument(sampleFileName);
            //Assert.NotNull(sampleDocument);

            //// Serialisiertes mit Sample vergleichen
            //compareXmlNodes(sampleDocument.DocumentElement, serializedDocument.DocumentElement);
        }



        [Fact]
        public void Serialize_old()
        {
            string outputFileName = "serializer_output.xml";

            var testObject = createTestRootObject();
            var serializer = createSerializer();

            using (var writer = new StreamWriter(outputFileName))
            {
                using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, IndentChars = "  ", Encoding = Encoding.UTF8 }))
                {
                    serializer.Serialize(xmlWriter, testObject);
                }
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(Root.GekidNamespace, SchemaFileName);
            settings.ValidationType = ValidationType.Schema;
        

            XmlDocument createdDocument = new XmlDocument();

            using (var reader = XmlReader.Create(outputFileName, settings))
            {
                createdDocument.Load(reader);

                List<string> errors = new List<string>();
                var eventHandler = new ValidationEventHandler((p, q) => errors.Add(q.Message));
                createdDocument.Validate(eventHandler);
                Assert.Empty(errors);
            }

            var sampleDocument = new XmlDocument();
            using (var reader = XmlReader.Create("sample.xml", settings))
            {
                sampleDocument.Load(reader);
            }

            compareXmlNodes(sampleDocument.DocumentElement, createdDocument.DocumentElement);
        }

        // <summary>
        /// Zum Test von ungültigen Enumerationswerten (NotSpecified)
        /// </summary>
        [Fact]
        public void SerializeMinimal_Test()
        {
            string outputFileName = "serializer_minimal_output.xml";
            string sampleFileName = "sample_minimal.xml";

            serialize(outputFileName, true);
            var serializedDocument = readDocument(outputFileName);

            Assert.NotNull(serializedDocument);

            testOnValidationErrors(serializedDocument);

            var sampleDocument = readDocument(sampleFileName);
            Assert.NotNull(sampleDocument);

            // Serialisiertes mit Sample vergleichen
            compareXmlNodes(sampleDocument.DocumentElement, serializedDocument.DocumentElement);
        }

        [Fact]
        public void Deserialize()
        {                      
            var sampleFile = "sample.xml";
            var objDeserialized = deserialize(sampleFile);

            Assert.NotNull(objDeserialized);
        }

        /// <summary>
        /// Deserialisiert das Sample, serialisiert es
        /// erneut und vergleicht es anschließend mit dem Original
        /// </summary>
        [Fact]
        public void DeserializeWithCompare()
        {            

            var sampleFile = "sample.xml";
            var reserializedFile = "reserialized_output.xml";

            var objDeserialized = deserialize(sampleFile);
            Assert.NotNull(objDeserialized);

            serialize(objDeserialized, reserializedFile);

            var sampleDocument = readDocument(sampleFile);            
            var reserializedDocument = readDocument(reserializedFile);

            // Re-serialisiertes Dokument mit Sample vergleichen
            compareXmlNodes(sampleDocument.DocumentElement, reserializedDocument.DocumentElement);
        }


        /// <summary>
        /// Deserialisiert das Minimal-Sample, serialisiert es
        /// erneut und vergleicht es anschließend mit dem Original.
        /// Das Minimal-Sample wird dazu verwendet um zu testen,
        /// ob leere Werte leer oder falsch serialisiert werden,
        /// wobei hier gerade bei den Enumerationen erhöhtes Risiko einer 
        /// falschen Serialisierung besteht
        /// </summary>
        [Fact]
        public void DeserializeMinimalWithCompare()
        {

            var sampleFile = "sample_minimal.xml";
            var reserializedFile = "reserialized_minimal_output.xml";

            var objDeserialized = deserialize(sampleFile);
            Assert.NotNull(objDeserialized);

            serialize(objDeserialized, reserializedFile);

            var sampleDocument = readDocument(sampleFile);
            var reserializedDocument = readDocument(reserializedFile);

            // Re-serialisiertes Dokument mit Sample vergleichen
            compareXmlNodes(sampleDocument.DocumentElement, reserializedDocument.DocumentElement);
        }

        private Root deserialize(string fileName)
        {
            var serializer = createSerializer();

            Root objDeserialized = null;
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                objDeserialized = serializer.Deserialize(reader) as Root;                             
            }
            return objDeserialized;
        }

        private void serialize(string outputFileName, bool minimalTest = false)
        {
            var testObject = minimalTest
                                ? createMinimalTestRootObject()
                                : createTestRootObject();

            serialize(testObject, outputFileName);
        }

        private void serialize(Root objToSerialize, string outputFileName)
        {
            var serializer = createSerializer();

            using (var writer = new StreamWriter(outputFileName))
            {
                using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true, IndentChars = "  ", Encoding = Encoding.UTF8 }))
                {
                    serializer.Serialize(xmlWriter, objToSerialize);
                }
            }
        }


        private void testOnValidationErrors(XmlDocument document)
        {          
            List<string> errors = new List<string>();
            var eventHandler = new ValidationEventHandler((p, q) => errors.Add(q.Message));
            document.Validate(eventHandler);
            Assert.Empty(errors);            
        }

        private XmlDocument readDocument(string fileName)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(Root.GekidNamespace, SchemaFileName);
            settings.ValidationType = ValidationType.Schema;

            XmlDocument loadedDocument = new XmlDocument();

            using (var reader = XmlReader.Create(fileName, settings))
            {
                loadedDocument.Load(reader);
            }

            return loadedDocument;
        }
        

       

        private void compareXmlNodes(XmlElement x, XmlElement y)
        {
            Assert.True(x.Name == y.Name, $"Names not equal. {x.Name} != {y.Name}");
            Assert.True(x.NamespaceURI == y.NamespaceURI, $"Namespaces of {x.Name} not equal.");
            Assert.True(x.Value == y.Value, $"Values of {x.Name} not equal.");

            var xAttributes = x.Attributes.OfType<XmlAttribute>().Where(p => p.Name != "xmlns" && p.Prefix != "xmlns").ToList();
            var yAttributes = y.Attributes.OfType<XmlAttribute>().Where(p => p.Name != "xmlns" && p.Prefix != "xmlns").ToList();

            Assert.True(xAttributes.Count == yAttributes.Count, $"In {x.Name} ungleiche Zahl von Attributen");
            Assert.False(xAttributes.OfType<XmlAttribute>()
                .Where((p, i) => yAttributes[i].Value != p.Value)
                .Any()
                , $"Ungleiche Attribute in {x.Name}.");

            var xChilds = x.ChildNodes.OfType<XmlElement>().Where(p => !p.IsEmpty && !p.Value.IsNothing()).ToList();
            var yChilds = y.ChildNodes.OfType<XmlElement>().Where(p => !p.IsEmpty && !p.Value.IsNothing()).ToList();
            Assert.True(xChilds.Count == yChilds.Count, $"Ungleiche Zahl von Unterknoten in {x.Name}");
            for(int i = 0; i< xChilds.Count; i++)
            {
                var xElm = xChilds[i];
                var yElm = yChilds[i];

                compareXmlNodes(xElm, yElm);
            }
        }

        private static Root createTestRootObject()
        {
            return new Root()
            {
                SchemaVersion = "2.0.0",
                Absender = new Absender()
                {
                    Id = "AGI8768",
                    InstallationsId = "KH9871",
                    SoftwareId = "98724697",
                    Anschrift = "Anschrift 1a",
                    Ansprechpartner = "Kai Niemand",
                    Telefon = "0900-555-1260",
                    Email = "email@example.com",
                    Bezeichnung = "Bezeichnung 1"
                },
                Melder = new MelderTyp[]
                  {
                      new MelderTyp()
                      {
                          MeldendeStelle = "Meldstelle1",
                          Id = "ID264",
                          BIC = "INGDDEFFXXX",
                          IBAN = "DE12500105170648489890",
                          LANR = "LANR2",
                          IKNR = "IKNR1",
                          BSNR = "BSNR1",
                          Anschrift = "Anschrift 2",
                          KlinikStationPraxis = "Station Praxis 1",
                          Arztname = "Arztname 1",
                          PLZ = "01234",
                          Ort = "Ort 7",
                          Bankname = "Bankname 4",
                          Kontoinhaber = "Inhaber 2"
                      },
                  },
                Patienten = new Patient[]
                    {
                        new Patient() {
                            Stammdaten = new Stammdaten()
                            {
                                Menge_Adresse = new Adresse[]
                                {
                                    new Adresse()
                                    {
                                        PLZ = "12864",
                                        Ort = "Ort 11",
                                        Strasse ="Strasse Bravo",
                                        Hausnummer ="17a",
                                        Land = "D",
                                        GueltigVon =new DatumTyp("03.02.1980"),
                                        GueltigBis=new DatumTyp("31.10.2007")
                                    }
                                },
                                FruehereNamen = new Collection<string> {"Geisler", "Reumann-Lüdinger"},
                                Id = "PatId43687628",
                                Vornamen = "Rudolf Clarence Fidelius",
                                Geburtsdatum = new DatumTyp(1971, 2, 11),
                                Nachname = "Sondermeyer",
                                Geschlecht = "m",
                                KrankenversichertenNr="Z629410041",
                                FamilienangehoerigenNr = "A123456789",
                                Geburtsname ="Birthname 1",
                                KrankenkassenNr="104940005",
                                Namenszusatz ="Namezusatz",
                                Titel ="Titel a"
                            },
                            Anmerkung = "Keine Anmerkung",
                            Meldungen= new Meldung[]
                            {
                                new Meldung()
                                {
                                    Anmerkung = "Keine Meldungsanmerkung",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "13.05.2016",
                                    MelderId = "ID264",
                                    Id = "Meldung3267",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum277",
                                        Diagnosedatum = "10.03.2014",
                                        IcdCode = "C44.0",
                                        Seitenlokalisation = "L"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum277",
                                        Datum = new DatumTyp("11.11.2015"),
                                        IcdCode = "C44.0",
                                        //IcdVersion = "GM_10_2014",
                                        IcdVersion = "10 2014 GM",
                                        Text = "Diagnosetext 11",
                                        IcdoCode = "C44.0",

                                        IcdoVersion = "32",
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item32,
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item31,
                                        IcdoFreitext = "str1234",
                                        Anmerkung = "Diagnoseanmerkung",
                                        //AllgemeinerLeistungszustandEnumValue = AllgemeinerLeistungszustandTyp.NotSpecified,
                                        AllgemeinerLeistungszustand = "80%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Bronchial-Ca.",
                                                IcdCode = "C34.0",
                                                IcdVersion = "2013",
                                                Diagnosedatum = "18.02.2013"
                                            }
                                        },
                                        Seitenlokalisation = "T",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo01",
                                                Datum = "10.03.2014",
                                                EinsendeNr = "str1234",
                                                Code = "8020/3",
                                                IcdOVersion = "32",
                                                Freitext = "str1234",
                                                Grading = "3",
                                                LkUntersucht = 33,
                                                LkBefallen = 34,
                                                SentinelLkUntersucht = 35,
                                                SentinelLkBefallen = 36
                                            }
                                        },
                                        Fernmetastasen = new Fernmetastase[]
                                        {
                                            new Fernmetastase()
                                            {
                                                Datum = "20.04.2014",
                                                Lokalisation = "PUL"
                                            }
                                        },
                                        TnmKlassifizierungKlinisch = new TnmTyp()
                                        {
                                                Datum = "20.03.2013",
                                                Version = 7,
                                                SymbolY = "y",
                                                SymbolR = "r",
                                                SymbolA = "a",
                                                PraefixT = "c",
                                                T = "T1a",
                                                SymbolM = "(m)",
                                                PraefixN = "c",
                                                N = "N1",
                                                PraefixM = "u",
                                                M = "M0",
                                                L = "L0",
                                                V = "V1",
                                                Pn = "PnX",
                                                S = "S3"
                                        },
                                         TnmKlassifizierungPathologisch = new TnmTyp()
                                        {
                                                Datum = "20.05.2013",
                                                Version = 7,
                                                SymbolY = "y",
                                                SymbolR = "r",
                                                SymbolA = "a",
                                                PraefixT = "p",
                                                T = "T1a",
                                                SymbolM = "(m)",
                                                PraefixN = "p",
                                                N = "N1",
                                                PraefixM = "u",
                                                M = "M0",
                                                L = "L0",
                                                V = "V1",
                                                Pn = "PnX",
                                                S = "S3"
                                        },
                                        WeitereKlassifikationen = new WeitereKlassifikation[]
                                        {
                                            new WeitereKlassifikation
                                            {
                                                Name = "Klasse 23",
                                                Datum = "11.03.2013",
                                                Stadium = "ABC"
                                            }
                                        }
                                    },
                                    Operationen = new Op[]
                                    {
                                        new Op
                                        {
                                            Intention="K",
                                            Histologie = new HistologieTyp()
                                            {
                                                Id = "histo23",
                                                Datum = "16.04.2013",
                                                EinsendeNr = "str1234",
                                                Code = "8130/3",
                                                IcdOVersion = "32",
                                                Freitext = "str1234",
                                                Grading = "4",
                                                LkUntersucht = 33,
                                                LkBefallen = 23,
                                                SentinelLkUntersucht = 13,
                                                SentinelLkBefallen = 3
                                            },
                                            TnmKlassifizierung = new TnmTyp
                                            {
                                                //Id = "ID3427",
                                                Datum = "16.04.2013",
                                                Version = 7,
                                                T = "T2b",
                                                N = "N0",
                                                M = "M1",
                                                L = "L1",
                                                V = "V2",
                                                Pn = "Pn1",
                                                S = "SX"

                                            },
                                            Residualstatus = new ResidualstatusTyp() { Lokal = RTyp.R1, Gesamt = RTyp.R0 },
                                            Komplikationen = new Collection<string> {"ABD", "HRS"},
                                            Operateure = new Collection<string> {"Mr. Proper", "Dr. Beckmann"},
                                            OpsCodes = new Collection<string> {"5-217.1", "5-812.00"},
                                            OpsVersion = "2013",
                                            Datum = "15.04.2013",
                                            Id = "Op31687",
                                            Anmerkung = "str1234"
                                        },
                                    },
                                    Strahlentherapien = new Strahlentherapie[]
                                    {
                                        new Strahlentherapie()
                                        {
                                            Bestrahlungen = new Bestrahlung[]
                                            {
                                                new Bestrahlung
                                                {
                                                    BeginnDatum = "01.05.2013",
                                                    EndeDatum = "10.05.2013",
                                                    Zielgebiet ="3.1.",
                                                    SeiteZielgebiet = "L",
                                                    Applikationsart = "M",
                                                    Einzeldosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 2
                                                       , Einheit = "Gy"
                                                    },
                                                    Gesamtdosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 23
                                                       , Einheit = "Gy"
                                                    },
                                                },
                                                new Bestrahlung
                                                {
                                                    BeginnDatum = "04.04.2015",
                                                    EndeDatum = "02.05.2015",
                                                    Zielgebiet ="5.7.1.",
                                                    SeiteZielgebiet = "M",
                                                    Applikationsart = "K",
                                                    //Einzeldosis = "1GBq",
                                                    //Gesamtdosis = "23GBq"
                                                    Einzeldosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 1
                                                       , Einheit = "GBq"
                                                    },
                                                    //Gesamtdosis = "23Gy"
                                                    Gesamtdosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 23
                                                       , Einheit = "GBq"
                                                    }

                                                },
                                            },
                                            Nebenwirkungen = new NebenwirkungTyp[]
                                            {
                                                new NebenwirkungTyp { Art = "Cough", Grad = "K", Version = "4" },
                                                new NebenwirkungTyp { Art = "Anemia", Grad = "4", Version = "4" },
                                            },
                                            EndeGrund = "E",
                                            Id = "StId4379872",
                                            Intention = "P",
                                            StellungOp = "A",
                                            Anmerkung = "Strahlen Anmerkung",
                                            Residualstatus = new ResidualstatusTyp {  Gesamt = RTyp.R1, Lokal = RTyp.R1},
                                        }
                                    },
                                    SystemischeTherapien = new SystemischeTherapie[]
                                    {
                                        new SystemischeTherapie
                                        {
                                            Id = "SystId2387",
                                            Intention = "K",
                                            StellungOp = "O",
                                            BeginnDatum = "10.05.2014",
                                            EndeDatum = "24.05.2014",
                                            EndeGrund = "E",
                                            Nebenwirkungen = new NebenwirkungTyp[]
                                            {
                                                new NebenwirkungTyp { Art = "Nausea", Grad = "3", Version = "4" },
                                            },
                                            Substanzen = new Collection<string> {"Subst Eins", "Subst Zwei"},
                                            TherapieArten = new Collection<string> { "IM", "HO"},
                                            TherapieartAnmerkung = "Art Anmerkung",
                                            Protokoll = "Prot303",
                                            Residualstatus = new ResidualstatusTyp() { Gesamt = RTyp.R0, Lokal = RTyp.R0 },
                                            Anmerkung = "Anmerkung Systemisch"
                                        }
                                    },
                                    Verlaeufe = new Verlauf[]
                                    {
                                        new Verlauf
                                        {
                                            Id = "Verlauf26876",
                                            WeitereKlassifikationen = new WeitereKlassifikation[]
                                            {
                                                new WeitereKlassifikation { Name = "Klass1", Stadium = "AH1", Datum = "01.10.2014" },
                                            },
                                            Fernmetastasen = new Fernmetastase[]
                                            {
                                                new Fernmetastase { Lokalisation = "PUL", Datum = "03.10.2014" },
                                                new Fernmetastase { Lokalisation = "OSS", Datum = "02.10.2014" },
                                            },
                                            //TnmKlassifizierungen = new TnmTyp[]
                                            //{
                                            //    new TnmTyp
                                            //    {
                                            //        Id = "tnm01",
                                            //        Datum = "01.09.2014",
                                            //        Version = 7,
                                            //        T = "T4",
                                            //        N = "N0",
                                            //        M = "M1",
                                            //    }
                                            //},
                                            TnmKlassifizierung = new TnmTyp
                                            {
                                                //Id = "tnm01",
                                                Datum = "01.09.2014",
                                                Version = 7,
                                                T = "T4",
                                                N = "N0",
                                                M = "M1",
                                            },
                                            Histologie = new HistologieTyp()
                                            {
                                                Id = "histo89",
                                                Datum = "11.09.2014",
                                                EinsendeNr = "str1234",
                                                Code = "8920/3",
                                                IcdOVersion = "32",
                                                Freitext = "str1234",
                                                Grading = "3",
                                                LkUntersucht = 33,
                                                LkBefallen = 22,
                                                SentinelLkUntersucht = 11,
                                                SentinelLkBefallen = 1
                                            },
                                            Datum = "13.09.2014",
                                            TumorstatusGesamt = "V",
                                            TumorstatusLokal = "U",
                                            TumorstatusLymphknoten = "X",
                                            TumorstatusFernmetastasen = "F",
                                            AllgemeinerLeistungszustand = "4",
                                            Tod = new VerlaufTod {
                                                Sterbedatum = "11.11.2011"
                                                , Tumorbedingt = JnuTyp.Ja
                                                , Todesursachen = new MengeTodesursache()
                                                {
                                                    UrsachenIcdCodes = new Collection<string> {"C22.0", "C23.0"}
                                                    , UrsachenIcdVersionEnumValue = IcdVersionTyp.GM_10_2014
                                                }
                                            },
                                            Anmerkung = "Anmerkung Verlauf"
                                        }
                                    },
                                    Zusatzitems = new ZusatzItem[]
                                    {
                                        new ZusatzItem { Art = "Untersuchungsanlass", Bemerkung = "Comment", Datum = "03.03.2012", Wert = "Z"}
                                    },
                                    Tumorkonferenzen = new Tumorkonferenz[]
                                    {
                                        new Tumorkonferenz{Datum = "08.06.2013", Id = "TB31268", Typ = "praeth", Anmerkung = "Anmerkung TB"}
                                    },
                                },
                                new Meldung()
                                {
                                    Anmerkung = "Schnellere Meldung",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "13.05.2018",
                                    MelderId = "ID264",
                                    Id = "Meldung3268",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum278",
                                        Diagnosedatum = "28.08.2017",
                                        IcdCode = "C50.4",
                                        Seitenlokalisation = "R"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum278",
                                        Datum = new DatumTyp("28.08.2017"),
                                        IcdCode = "C50.4",
                                        //IcdVersion = "GM_10_2014",
                                        IcdVersion = "10 2017 GM",
                                        Text = "Diagnosetext Mamma",
                                        IcdoCode = "C50.4",

                                        IcdoVersion = "32",
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item32,
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item31,
                                        IcdoFreitext = "str1234",
                                        Anmerkung = "Diagnoseanmerkung",
                                        //AllgemeinerLeistungszustandEnumValue = AllgemeinerLeistungszustandTyp.NotSpecified,
                                        AllgemeinerLeistungszustand = "90%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Melanom",
                                                IcdCode = "C43.7",
                                                IcdVersion = "Sonstige",
                                                Diagnosedatum = "01.01.1999"
                                            }
                                        },
                                        Seitenlokalisation = "R",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo01",
                                                Datum = "10.07.2017",
                                                EinsendeNr = "str987654",
                                                Code = "8720/3",
                                                IcdOVersion = "32",
                                                Freitext = "str987654",
                                                Grading = "3",
                                                LkUntersucht = 33,
                                                LkBefallen = 34,
                                                SentinelLkUntersucht = 35,
                                                SentinelLkBefallen = 36
                                            }
                                        },
                                        TnmKlassifizierungKlinisch = new TnmTyp()
                                        {
                                                Datum = "20.09.2017",
                                                Version = 7,
                                                SymbolY = null,
                                                SymbolR = "r",
                                                SymbolA = "",
                                                PraefixT = "c",
                                                T = "T1",
                                                SymbolM = "",
                                                PraefixN = "p",
                                                N = "N0",
                                                PraefixM = "u",
                                                M = "M0",
                                                L = "",
                                                V = "",
                                                Pn = "",
                                                S = ""
                                        },
                                        TnmKlassifizierungPathologisch = new TnmTyp()
                                        {
                                                Datum = "23.09.2017",
                                                Version = 7,
                                                SymbolY = null,
                                                SymbolR = null,
                                                SymbolA = null,
                                                PraefixT = "p",
                                                T = "T1a",
                                                SymbolM = null,
                                                PraefixN = "p",
                                                N = "N1",
                                                PraefixM = "u",
                                                M = null,
                                                L = null,
                                                V = null,
                                                Pn = null,
                                                S = null
                                        },
                                        WeitereKlassifikationen = new WeitereKlassifikation[]
                                        {
                                            new WeitereKlassifikation
                                            {
                                                Name = "Klasse 23",
                                                Datum = "11.03.2013",
                                                Stadium = "ABC"
                                            }
                                        },
                                        ModulMammaSection = new ModulMamma()
                                        {
                                            PraethMenopausenstatusEnumValue = MammaPraethMenospausenstatus.PraeAndPerimenopausal
                                            , HormonrezeptorStatusOestrogenEnumValue = MammaHormonrezeptor.Positiv
                                            , HormonrezeptorStatusProgesteronEnumValue = MammaHormonrezeptor.Negativ
                                            , IntraopPraeparatkontrolleEnumValue = MammaIntraopPraeparatkontrolle.Unbekannt
                                            , PraeopDrahtmarkierungEnumValue = MammaPraeopDrahtmarkierung.Unbekannt
                                            , TumorgroesseInvasiv = "23"
                                            , TumorgroesseDCIS = "19"

                                        }
                                    },

                                },
                                new Meldung()
                                {
                                    Anmerkung = "Schnellere Meldung",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "13.05.2018",
                                    MelderId = "ID264",
                                    Id = "Meldung3269",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum279",
                                        Diagnosedatum = "28.09.2017",
                                        IcdCode = "C50.1",
                                        Seitenlokalisation = "T"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum278",
                                        Datum = new DatumTyp("28.09.2017"),
                                        IcdCode = "C50.1",
                                        //IcdVersion = "GM_10_2014",
                                        IcdVersion = "10 2017 GM",
                                        Text = "Diagnosetext Mamma2",
                                        IcdoCode = "C50.1",

                                        IcdoVersion = "32",
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item32,
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item31,
                                        IcdoFreitext = "ICD-O-Freitext",
                                        Anmerkung = "Diagnoseanmerkung",
                                        //AllgemeinerLeistungszustandEnumValue = AllgemeinerLeistungszustandTyp.NotSpecified,
                                        AllgemeinerLeistungszustand = "90%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Melanom",
                                                IcdCode = "C43.7",
                                                IcdVersion = "Sonstige",
                                                Diagnosedatum = "01.01.1999"
                                            }
                                        },
                                        Seitenlokalisation = "T",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo01",
                                                Datum = "10.07.2017",
                                                EinsendeNr = "str987654",
                                                Code = "8480/3",
                                                IcdOVersion = "32",
                                                Freitext = "str987654",
                                                Grading = "2",
                                                LkUntersucht = 33,
                                                LkBefallen = 34,
                                                SentinelLkUntersucht = 35,
                                                SentinelLkBefallen = 36
                                            }
                                        },
                                        TnmKlassifizierungKlinisch = new TnmTyp()
                                        {
                                                Datum = "20.09.2017",
                                                Version = 7,
                                                SymbolY = null,
                                                SymbolR = "r",
                                                SymbolA = "",
                                                PraefixT = "c",
                                                T = "T1c",
                                                SymbolM = "",
                                                PraefixN = "p",
                                                N = "N0",
                                                PraefixM = "u",
                                                M = "M1",
                                                L = "",
                                                V = "",
                                                Pn = "",
                                                S = ""
                                        },
                                        TnmKlassifizierungPathologisch = new TnmTyp()
                                        {
                                                Datum = "23.10.2017",
                                                Version = 7,
                                                SymbolY = null,
                                                SymbolR = null,
                                                SymbolA = null,
                                                PraefixT = "p",
                                                T = "T1a",
                                                SymbolM = null,
                                                PraefixN = "p",
                                                N = "N1",
                                                PraefixM = "u",
                                                M = null,
                                                L = null,
                                                V = null,
                                                Pn = null,
                                                S = null
                                        },
                                        WeitereKlassifikationen = new WeitereKlassifikation[]
                                        {
                                            new WeitereKlassifikation
                                            {
                                                Name = "Klasse 23",
                                                Datum = "11.03.2013",
                                                Stadium = "ABC"
                                            }
                                        },
                                        ModulMammaSection = new ModulMamma()
                                        {
                                            PraethMenopausenstatus= "1"
                                            , HormonrezeptorStatusOestrogen= "P"
                                            , HormonrezeptorStatusProgesteron = "N"
                                            , IntraopPraeparatkontrolle = "U"
                                            , PraeopDrahtmarkierung = "U"
                                            , TumorgroesseInvasiv = "23"
                                            , TumorgroesseDCIS = "19"

                                        }
                                    },

                                },
                                new Meldung()
                                {
                                    Anmerkung = "Meldung mit Modul Darm",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "28.08.2020",
                                    MelderId = "ID264",
                                    Id = "Meldung3270",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum279",
                                        Diagnosedatum = "23.06.2019",
                                        IcdCode = "C20",
                                        Seitenlokalisation = "T"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum278",
                                        Datum = new DatumTyp("23.06.2019"),
                                        IcdCode = "C20",
                                        //IcdVersion = "GM_10_2014",
                                        IcdVersion = "10 2018 GM",
                                        Text = "Diagnosetext Darm",
                                        IcdoCode = "C20.9",

                                        IcdoVersion = "32",
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item32,
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item31,
                                        IcdoFreitext = "str1234",
                                        Anmerkung = "Diagnoseanmerkung",
                                        //AllgemeinerLeistungszustandEnumValue = AllgemeinerLeistungszustandTyp.NotSpecified,
                                        AllgemeinerLeistungszustand = "70%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Melanom",
                                                IcdCode = "C43.7",
                                                IcdVersion = "Sonstige",
                                                Diagnosedatum = "01.01.1999"
                                            }
                                        },
                                        Seitenlokalisation = "T",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo02",
                                                Datum = "18.06.2019",
                                                EinsendeNr = "str20190610",
                                                Code = "8140/3",
                                                IcdOVersion = "32",
                                                Freitext = "Adenokarzinom o.n.A.",
                                                Grading = "2",
                                            }
                                        },
                                        ModulDarmSection = new ModulDarm()
                                        {
                                            RektumAbstandAnokutanlinie = "95",
                                            RektumAbstandAboralerResektionsrand = "20",
                                            RektumAbstandCircResektionsebene = "2",
                                            //RektumQualitaetTME = "P",
                                            RektumQualitaetTMEEnumValue = DarmRektumQualitaetTME.PME,
                                            RektumMRTDuennschichtAngabemesorektaleFaszie = "D",
                                            //ArtEingriffEnumValue = DarmArtEingriff.Elektiv,
                                            ArtEingriff = "E",
                                            //RektumAnzeichnungStomapositionEnumValue = DarmRektumAnzeichnungStomaposition.Durchgefuehrt
                                            RektumAnzeichnungStomaposition = "D"
                                        },
                                        ModulAllgemeinSection = new ModulAllgemein()
                                        {
                                            DatumSozialdienstkontakt = new DatumNuTyp(DatumNuNonNumericValues.N),
                                            DatumStudienrekrutierung = new DatumNuTyp(DatumNuNonNumericValues.U)
                                        }
                                    },

                                },
                                new Meldung()
                                {
                                    Anmerkung = "Meldung mit Modul Prostata",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "31.08.2020",
                                    MelderId = "ID264",
                                    Id = "Meldung3271",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum280",
                                        Diagnosedatum = "14.11.2019",
                                        IcdCode = "C61",
                                        Seitenlokalisation = "T"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum278",
                                        Datum = new DatumTyp("14.11.2019"),
                                        IcdCode = "C61",
                                        //IcdVersion = "GM_10_2014",
                                        IcdVersion = "10 2018 GM",
                                        Text = "Diagnosetext Prostata",
                                        IcdoCode = "C61.9",

                                        IcdoVersion = "32",
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item32,
                                        //IcdoVersionEnumValue = TopographieIcdOVersionTyp.Item31,
                                        IcdoFreitext = "str1234",
                                        Anmerkung = "Diagnoseanmerkung Prostata",
                                        //AllgemeinerLeistungszustandEnumValue = AllgemeinerLeistungszustandTyp.NotSpecified,
                                        AllgemeinerLeistungszustand = "100%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Melanom",
                                                IcdCode = "C43.7",
                                                IcdVersion = "Sonstige",
                                                Diagnosedatum = "01.01.1999"
                                            }
                                        },
                                        Seitenlokalisation = "T",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo02",
                                                Datum = "14.11.2019",
                                                EinsendeNr = "str20191114",
                                                Code = "8140/3",
                                                IcdOVersion = "32",
                                                Freitext = "Adenokarzinom o.n.A.",
                                                Grading = "2",
                                            }
                                        },
                                        ModulProstataSection = new ModulProstata()
                                        {
                                            GleasonScore = new ProstataGleasonScore()
                                            {
                                                GleasonGradPrimaer = "4",
                                                GleasonGradSekundaer = "3",
                                                GleasonScoreErgebnis = "7"
                                            },
                                            AnlassGleasonScoreEnumValue = ProstataAnlassGleasonScore.OP,
                                            DatumStanzen = new System.DateTime(2019,11,10),
                                            AnzahlPositiveStanzen = 4,
                                            AnzahlStanzen = 12,
                                            PSA = new decimal(40.3),
                                            DatumPSA =  new System.DateTime(2019,11,10),
                                            KomplikationPostOp = JnuTyp.Nein,
                                            CaBefallStanze = new ProstataCaBefallStanze(80)                                                                                        
                                        },
                                        ModulAllgemeinSection = new ModulAllgemein()
                                        {
                                            DatumSozialdienstkontakt = new DatumNuTyp("03.07.2020"),
                                            DatumStudienrekrutierung = new DatumNuTyp("15.08.2020")                                            
                                        }
                                    },
                                },
                                new Meldung()
                                {
                                    Anmerkung = "Meldung2 mit Modul Prostata",
                                    Anlass = Meldeanlass.Diagnose,
                                    Datum = "01.09.2020",
                                    MelderId = "ID264",
                                    Id = "Meldung3272",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum280",
                                        Diagnosedatum = "14.12.2019",
                                        IcdCode = "C61",
                                        Seitenlokalisation = "T"
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum279",
                                        Datum = new DatumTyp("14.12.2019"),
                                        IcdCode = "C61",                                        
                                        IcdVersion = "10 2018 GM",
                                        Text = "Diagnosetext Prostata2",
                                        IcdoCode = "C61.9",

                                        IcdoVersion = "32",                                        
                                        IcdoFreitext = "str1234",
                                        Anmerkung = "Diagnoseanmerkung Prostata2",                                        
                                        AllgemeinerLeistungszustand = "70%",
                                        FruehereTumorerkrankungen = new FruehereTumorerkrankung[]
                                        {
                                            new FruehereTumorerkrankung()
                                            {

                                                Freitext = "Melanom",
                                                IcdCode = "C43.7",
                                                IcdVersion = "Sonstige",
                                                Diagnosedatum = "01.01.1999"
                                            }
                                        },
                                        Seitenlokalisation = "T",
                                        Diagnosesicherung = "2",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo02",
                                                Datum = "14.12.2019",
                                                EinsendeNr = "str20191214",
                                                Code = "8140/3",
                                                IcdOVersion = "32",
                                                Freitext = "Adenokarzinom o.n.A.",
                                                Grading = "2",
                                            }
                                        },
                                        ModulProstataSection = new ModulProstata()
                                        {
                                            GleasonScore = new ProstataGleasonScore()
                                            {
                                                GleasonGradPrimaer = "4",
                                                GleasonGradSekundaer = "5",
                                                GleasonScoreErgebnis = "9"
                                            },
                                            AnlassGleasonScoreEnumValue = ProstataAnlassGleasonScore.OP,
                                            DatumStanzen = new System.DateTime(2019,11,10),
                                            AnzahlPositiveStanzen = 4,
                                            AnzahlStanzen = 12,
                                            PSA = new decimal(40.3),
                                            DatumPSA =  new System.DateTime(2019,11,10),
                                            KomplikationPostOp = JnuTyp.Nein,
                                            CaBefallStanze = new ProstataCaBefallStanze(ProstataCaBefallStanzeEnum.Unbekannt)                                                                                       
                                        },
                                        ModulAllgemeinSection = new ModulAllgemein()
                                        {
                                            DatumSozialdienstkontakt = new DatumNuTyp("03.07.2020"),
                                            DatumStudienrekrutierung = new DatumNuTyp("15.08.2020")
                                        }
                                    },

                                }
                            }
                        }
                    }
            };
        }

        private static Root createMinimalTestRootObject()
        {


            return new Root()
            {
                SchemaVersion = "2.0.0",
                Absender = new Absender()
                {
                    Id = "AGI8768",
                    InstallationsId = "KH9871",
                    SoftwareId = "98724697",
                    Anschrift = "Anschrift 1a",
                    Ansprechpartner = "Kai Niemand",
                    Telefon = "0900-555-1260",
                    Email = "email@example.com",
                    Bezeichnung = "Bezeichnung 1"
                },
                Melder = new MelderTyp[]
                  {
                      new MelderTyp()
                      {
                          MeldendeStelle = "Meldstelle1",
                          Id = "ID264",                        
                      },
                  },
                Patienten = new Patient[]
                    {
                        new Patient() {
                            Stammdaten = new Stammdaten()
                            {
                                Menge_Adresse = new Adresse[]
                                {
                                    new Adresse()
                                    {
                                        PLZ = "12864",
                                        Ort = "Ort 11",
                                        Strasse ="Strasse Bravo",                                        
                                        Land = "D",                                        
                                    }
                                },                                
                                Id = "PatId43687628",
                                Vornamen = "Rudolf Clarence Fidelius",
                                Geburtsdatum = new DatumTyp(1971, 2, 11),
                                Nachname = "Sondermeyer",
                                Geschlecht = "m",
                                KrankenversichertenNr="Z629410041",                                                                
                                KrankenkassenNr="104940005",                                                                
                            },
                            Anmerkung = "Keine Anmerkung",
                            Meldungen= new Meldung[]
                            {
                                new Meldung()
                                {
                                    Anmerkung = "Keine Meldungsanmerkung",                                                                      
                                    MelderId = "ID264",
                                    Id = "Meldung3267",
                                    Begruendung = "I",
                                    Tumorzuordnung = new Tumorzuordnung()
                                    {
                                        Id = "tum277",
                                        Diagnosedatum = "10.03.2014",
                                        IcdCode = "C44.0",                                        
                                    },
                                    Diagnose = new Diagnose()
                                    {
                                        Id = "tum277",
                                        Datum = new DatumTyp("11.11.2015"),
                                        IcdCode = "C44.0",
                                        IcdVersion = "GM_10_2014",                                      
                                        IcdoCode = "C44.0",
                                        IcdoVersion = "32",                                        
                                        Diagnosesicherung = "4",
                                        Histologien = new HistologieTyp[]
                                        {
                                            new HistologieTyp()
                                            {
                                                Id = "histo01",                                               
                                                EinsendeNr = "str1234",
                                                Code = "8020/3",
                                                IcdOVersion = "32",                                             
                                            }
                                        },
                                        Fernmetastasen = new Fernmetastase[]
                                        {
                                            new Fernmetastase()
                                            {
                                                Datum = "20.04.2014",
                                                Lokalisation = "PUL"
                                            }
                                        },
                                       
                                        TnmKlassifizierungKlinisch = new TnmTyp()
                                        {
                                                //Id = "tnm7638",
                                                Datum = "20.03.2013",
                                                Version = 7,
                                               
                                                PraefixT = "c",
                                                T = "T1a",                                              
                                                PraefixN = "p",
                                                N = "N1",
                                                PraefixM = "u",                                          
                                        },
                                         TnmKlassifizierungPathologisch = new TnmTyp()
                                        {
                                                //Id = "tnm7639",
                                                Datum = "20.05.2013",
                                                Version = 7,                                              
                                                PraefixT = "p",
                                                T = "T1a",                                             
                                                PraefixN = "p",
                                                N = "N1",
                                                PraefixM = "u",
                                                M = "M0",                                              
                                        },
                                        WeitereKlassifikationen = new WeitereKlassifikation[]
                                        {
                                            new WeitereKlassifikation
                                            {
                                                Name = "Klasse 23",
                                                Datum = "11.03.2013",
                                                Stadium = "ABC"
                                            }
                                        }
                                    },
                                    Operationen = new Op[]
                                    {
                                        new Op
                                        {
                                            Intention="K",
                                            Histologie = new HistologieTyp()
                                            {
                                                Id = "histo23",                                               
                                                Code = "8130/3",
                                                IcdOVersion = "32",
                                                Freitext = "str1234",
                                                Grading = "4",                                            
                                                LkBefallen = 23,                                               
                                                SentinelLkBefallen = 3
                                            },
                                            TnmKlassifizierung = new TnmTyp
                                            {
                                                //Id = "ID3427",
                                                Datum = "16.04.2013",
                                                Version = 7,
                                                T = "T2b",
                                                N = "N0",
                                                M = "M1",                                                                                               
                                            },
                                            Residualstatus = new ResidualstatusTyp() { Lokal = RTyp.R1 },                                                                                     
                                            OpsCodes = new Collection<string> {"5-217.1", "5-812.00"},
                                            OpsVersion = "2013",
                                            Datum = "15.04.2013",
                                            Id = "Op31687",                                            
                                        },
                                    },
                                    Strahlentherapien = new Strahlentherapie[]
                                    {
                                        new Strahlentherapie()
                                        {
                                            Bestrahlungen = new Bestrahlung[]
                                            {
                                                new Bestrahlung
                                                {
                                                    BeginnDatum = "01.05.2013",                                                    
                                                    Zielgebiet ="3.1.",                                                    
                                                    Applikationsart = "M",                                                    
                                                    Einzeldosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 2
                                                       , Einheit = "Gy"
                                                    },                                                    
                                                    Gesamtdosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 23
                                                         , Einheit = "Gy"
                                                    },
                                                },
                                                new Bestrahlung
                                                {
                                                    BeginnDatum = "04.04.2015",
                                                    Zielgebiet ="6.1.",
                                                    Applikationsart = "K",                                                   
                                                },
                                            },                                            
                                            Id = "StId4379872",
                                            Intention = "P",                                           
                                            Residualstatus = new ResidualstatusTyp {  },
                                        }
                                    },
                                    SystemischeTherapien = new SystemischeTherapie[]
                                    {
                                        new SystemischeTherapie
                                        {
                                            Id = "SystId2387",
                                            Intention = "K",
                                            BeginnDatum = "10.05.2014",
                                            Nebenwirkungen = new NebenwirkungTyp[]
                                            {
                                                new NebenwirkungTyp { Grad = "3", Version = "4" },
                                            },
                                            Substanzen = new Collection<string> {"Subst Eins"},
                                            TherapieArten = new Collection<string> { "IM"},                                                                                        
                                            Anmerkung = "Anmerkung Systemisch"
                                        }
                                    },
                                    Verlaeufe = new Verlauf[]
                                    {
                                        new Verlauf
                                        {
                                            Id = "Verlauf26876",
                                            WeitereKlassifikationen = new WeitereKlassifikation[]
                                            {
                                                new WeitereKlassifikation { Name = "Klass1", Stadium = "AH1", Datum = "01.10.2014" },
                                                new WeitereKlassifikation {  },
                                            },
                                                                                    
                                            TnmKlassifizierung = new TnmTyp
                                            {
                                                //Id = "tnm01",
                                                Datum = "01.09.2014",
                                                Version = 7,
                                                T = "T4",
                                                N = "N0",
                                                M = "M1",
                                            },
                                            Histologie = new HistologieTyp()
                                            {
                                                Id = "histo89",
                                                Datum = "11.09.2014",
                                                EinsendeNr = "str1234",
                                                Code = "8920/3",
                                                IcdOVersion = "32",                                               
                                            },
                                            Datum = "13.09.2014",
                                            TumorstatusGesamt = "V",
                                         
                                           
                                        }
                                        , new Verlauf
                                        {
                                            Id = "Verlauf26877"
                                            , Tod = new VerlaufTod {
                                                Sterbedatum = "11.11.2011"                                              
                                            },
                                        }
                                    },
                                    Zusatzitems = new ZusatzItem[]
                                    {
                                        new ZusatzItem { Art = "Untersuchungsanlass", Bemerkung = "Comment", Datum = "03.03.2012", Wert = "Z"}
                                        , new ZusatzItem { }
                                    },
                                    Tumorkonferenzen = new Tumorkonferenz[]
                                    {
                                        new Tumorkonferenz
                                        {
                                            Datum = "08.06.2013",
                                            Id = "TB31268",                                           
                                        }
                                    },
                                }
                            }
                        }
                    }
            };
        }

        private static XmlSerializer createSerializer() => new XmlSerializer(typeof(Root));
    }
}
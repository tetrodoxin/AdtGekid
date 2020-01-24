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
    public class SerializerTests
    {
        public const string SchemaFileName = @"ADT_GEKID_v2.0.0.xsd";

        [Fact]
        public void Deserialize()
        {
            var serializer = createSerializer();          

            var sampleFile = "sample.xml";            
            using (var reader = new StreamReader(sampleFile, Encoding.UTF8))
            {
                var obj = serializer.Deserialize(reader) as Root;
                Assert.NotNull(obj);
                
           }            
        }
        
        
        [Fact]
        public void Serialize()
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

            var testObject = createMinimalTestRootObject();
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
                          BIC="INGDDEFFXXX",
                          IBAN ="DE12500105170648489890",
                          LANR ="LANR2",
                          IKNR="IKNR1",
                          BSNR ="BSNR1",
                          Anschrift = "Anschrift 2",
                          KlinikStationPraxis = "Station Praxis 1",
                          Arztname="Arztname 1",
                          PLZ ="01234",
                          Ort ="Ort 7",
                          Bankname ="Bankname 4",
                          Kontoinhaber ="Inhaber 2"
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
                                        IcdVersion = "GM_10_2014",
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
                                        //TnmKlassifizierungen = new TnmTyp[]
                                        //{
                                        //    new TnmTyp
                                        //    {
                                        //        Id = "tnm7638",
                                        //        Datum = "20.03.2013",
                                        //        Version = 7,
                                        //        SymbolY = "y",
                                        //        SymbolR = "r",
                                        //        SymbolA = "a",
                                        //        PraefixT = "c",
                                        //        T = "T1a",
                                        //        SymbolM = "(m)",
                                        //        PraefixN = "p",
                                        //        N = "N1",
                                        //        PraefixM = "u",
                                        //        M = "M0",
                                        //        L = "L0",
                                        //        V = "V1",
                                        //        Pn = "PnX",
                                        //        S = "S3"
                                        //    }
                                        //},
                                        TnmKlassifizierungKlinisch = new TnmTyp()
                                        {
                                                Id = "tnm7638",
                                                Datum = "20.03.2013",
                                                Version = 7,
                                                SymbolY = "y",
                                                SymbolR = "r",
                                                SymbolA = "a",
                                                PraefixT = "c",
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
                                         TnmKlassifizierungPathologisch = new TnmTyp()
                                        {
                                                Id = "tnm7639",
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
                                                Id = "ID3427",
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
                                                    //Einzeldosis = "2Gy",
                                                    Einzeldosis = new StrahlendosisTyp()
                                                    {
                                                       Dosis = 2
                                                       , Einheit = "Gy"                                                        
                                                    },
                                                    //Gesamtdosis = "23Gy"
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
                                                    Zielgebiet ="6.1.",
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
                                                Id = "tnm01",
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
                                                Id = "tnm7638",
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
                                                Id = "tnm7639",
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
                                                Id = "ID3427",
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
                                                Id = "tnm01",
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
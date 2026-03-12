using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;

namespace mame
{
    public class State
    {
        public delegate void savestate_delegate(BinaryWriter sw);
        public delegate void loadstate_delegate(BinaryReader sr);
        public static savestate_delegate savestate_callback;
        public static loadstate_delegate loadstate_callback;
        public static void state_init()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                    savestate_callback = CPS.SaveStateBinary_cps1;
                    loadstate_callback = CPS.LoadStateBinary_cps1;
                    break;
                case "CPS-1(QSound)":
                    savestate_callback = CPS.SaveStateBinary_cps1_qsound;
                    loadstate_callback = CPS.LoadStateBinary_cps1_qsound;
                    break;
                case "CPS2":
                    savestate_callback = CPS.SaveStateBinary_cps2;
                    loadstate_callback = CPS.LoadStateBinary_cps2;
                    break;
                case "CPS2turbo":
                    savestate_callback = CPS.SaveStateBinary_cps2turbo;
                    loadstate_callback = CPS.LoadStateBinary_cps2turbo;
                    break;
                case "Data East":
                    switch (Machine.sName)
                    {
                        case "pcktgal":
                        case "pcktgalb":
                        case "pcktgal2":
                        case "pcktgal2j":
                        case "spool3":
                        case "spool3i":
                            savestate_callback = Dataeast.SaveStateBinary_pcktgal;
                            loadstate_callback = Dataeast.LoadStateBinary_pcktgal;
                            break;
                    }
                    break;
                case "Tehkan":
                    switch (Machine.sName)
                    {
                        case "pbaction":
                        case "pbaction2":
                        case "pbaction3":
                        case "pbaction4":
                        case "pbaction5":
                            savestate_callback = Tehkan.SaveStateBinary_pbaction;
                            loadstate_callback = Tehkan.LoadStateBinary_pbaction;
                            break;
                    }
                    break;
                case "Neo Geo":
                    savestate_callback = Neogeo.SaveStateBinary;
                    loadstate_callback = Neogeo.LoadStateBinary;
                    break;
                case "Technos":
                    switch (Machine.sName)
                    {
                        case "ddragon":
                        case "ddragonw":
                        case "ddragonw1":
                        case "ddragonu":
                        case "ddragonua":
                        case "ddragonub":
                        case "ddragonb2":
                        case "ddragonb":
                        case "ddragonba":
                            savestate_callback = Technos.SaveStateBinary_ddragon;
                            loadstate_callback = Technos.LoadStateBinary_ddragon;
                            break;
                        case "ddragon2":
                        case "ddragon2u":
                        case "ddragon2b":
                            savestate_callback = Technos.SaveStateBinary_ddragon2;
                            loadstate_callback = Technos.LoadStateBinary_ddragon2;
                            break;
                        case "tstrike":
                        case "tstrikea":
                        case "ddungeon":
                        case "ddungeone":
                        case "darktowr":
                            savestate_callback = Technos.SaveStateBinary_darktowr;
                            loadstate_callback = Technos.LoadStateBinary_darktowr;
                            break;
                        case "toffy":
                        case "stoffy":
                        case "stoffyu":
                            savestate_callback = Technos.SaveStateBinary_toffy;
                            loadstate_callback = Technos.LoadStateBinary_toffy;
                            break;
                    }
                    break;
                case "Tad":
                    switch (Machine.sName)
                    {
                        case "toki":
                        case "tokiu":
                        case "tokip":
                        case "tokia":
                        case "tokiua":
                        case "juju":
                        case "jujuba":
                            savestate_callback = Tad.SaveStateBinary_tad_toki;
                            loadstate_callback = Tad.LoadStateBinary_tad_toki;
                            break;
                        case "tokib":
                        case "jujub":
                            savestate_callback = Tad.SaveStateBinary_tad_tokib;
                            loadstate_callback = Tad.LoadStateBinary_tad_tokib;
                            break;
                    }
                    break;
                case "Megasys1":
                    switch (Machine.sName)
                    {
                        case "lomakai"://Z
                        case "makaiden":
                            savestate_callback = Megasys1.SaveStateBinary_z;
                            loadstate_callback = Megasys1.LoadStateBinary_z;
                            break;
                        case "p47"://A
                        case "p47j":
                        case "p47je":
                        case "kickoff":
                        case "tshingen":
                        case "tshingena":
                        case "kazan":
                        case "iganinju":
                        case "astyanax":
                        case "lordofk":
                        case "hachoo":
                        case "jitsupro":
                        case "plusalph":
                        case "stdragon":
                        case "stdragona":
                        case "stdragonb":
                        case "rodland":
                        case "rodlandj":
                        case "rittam":
                        case "rodlandjb":
                        case "phantasm":
                        case "edfp":
                        case "soldam":
                        case "soldamj":
                            savestate_callback = Megasys1.SaveStateBinary_a;
                            loadstate_callback = Megasys1.LoadStateBinary_a;
                            break;
                        case "avspirit"://B
                        case "monkelf":
                        case "edf":
                        case "edfa":
                        case "edfu":
                        case "hayaosi1":
                        //case "edfbl":
                            savestate_callback = Megasys1.SaveStateBinary_b;
                            loadstate_callback = Megasys1.LoadStateBinary_b;
                            break;
                        case "64street"://C
                        case "64streetj":
                        case "64streetja":
                        case "bigstrik":
                        case "chimerab":
                        case "cybattlr":
                            savestate_callback = Megasys1.SaveStateBinary_c;
                            loadstate_callback = Megasys1.LoadStateBinary_c;
                            break;
                        case "peekaboo"://D
                        case "peakaboou":
                            savestate_callback = Megasys1.SaveStateBinary_d;
                            loadstate_callback = Megasys1.LoadStateBinary_d;
                            break;
                    }
                    break;
                case "Gaelco":
                    switch (Machine.sName)
                    {
                        case "bigkarnk":
                            savestate_callback = Gaelco.SaveStateBinary_bigkarnk;
                            loadstate_callback = Gaelco.LoadStateBinary_bigkarnk;
                            break;
                        case "biomtoy":
                        case "biomtoya":
                        case "biomtoyb":
                        case "biomtoyc":
                        case "bioplayc":
                        case "maniacsp":
                        case "lastkm":
                        case "squash":
                        case "thoop":
                            savestate_callback = Gaelco.SaveStateBinary_maniacsq;
                            loadstate_callback = Gaelco.LoadStateBinary_maniacsq;
                            break;
                    }
                    break;
                case "Namco System 1":
                    savestate_callback = Namcos1.SaveStateBinary;
                    loadstate_callback = Namcos1.LoadStateBinary;
                    break;
                case "IGS011":
                    savestate_callback = IGS011.SaveStateBinary;
                    loadstate_callback = IGS011.LoadStateBinary;
                    break;
                case "PGM":
                    savestate_callback = PGM.SaveStateBinary;
                    loadstate_callback = PGM.LoadStateBinary;
                    break;
                case "M72":
                    savestate_callback = M72.SaveStateBinary;
                    loadstate_callback = M72.LoadStateBinary;
                    break;
                case "M92":
                    savestate_callback = M92.SaveStateBinary;
                    loadstate_callback = M92.LoadStateBinary;
                    break;
                case "Taito":
                    switch (Machine.sName)
                    {
                        case "tokio":
                        case "tokioo":
                        case "tokiou":
                        case "tokiob":
                            savestate_callback = Taito.SaveStateBinary_tokio;
                            loadstate_callback = Taito.LoadStateBinary_tokio;
                            break;
                        case "bublbobl":
                        case "bublbobl1":
                        case "bublboblr":
                        case "bublboblr1":                        
                        case "bublcave":
                        case "bublcave11":
                        case "bublcave10":
                            savestate_callback = Taito.SaveStateBinary_bublbobl;
                            loadstate_callback = Taito.LoadStateBinary_bublbobl;
                            break;
                        case "boblbobl":
                        case "sboblbobl":
                        case "sboblbobla":
                        case "sboblboblb":
                        case "sboblbobld":
                        case "sboblboblc":
                        case "dland":
                        case "bbredux":
                        case "bublboblb":
                        case "boblcave":
                            savestate_callback = Taito.SaveStateBinary_boblbobl;
                            loadstate_callback = Taito.LoadStateBinary_boblbobl;
                            break;
                        case "bub68705":
                            savestate_callback = Taito.SaveStateBinary_bub68705;
                            loadstate_callback = Taito.LoadStateBinary_bub68705;
                            break;
                        case "opwolf":
                        case "opwolfa":
                        case "opwolfj":
                        case "opwolfu":
                        case "opwolfb":
                        case "opwolfp":
                            savestate_callback = Taito.SaveStateBinary_opwolf;
                            loadstate_callback = Taito.LoadStateBinary_opwolf;
                            break;
                    }
                    break;
                case "Taito B":
                    switch (Machine.sName)
                    {
                        case "masterw":
                        case "masterwu":
                        case "masterwj":
                        case "yukiwo":
                        case "tetrista":
                            savestate_callback = Taitob.SaveStateBinary_masterw;
                            loadstate_callback = Taitob.LoadStateBinary_masterw;
                            break;
                        case "nastar":
                        case "nastarw":
                        case "rastsag2":
                        case "rambo3":
                        case "rambo3u":
                        case "rambo3p":
                        case "crimec":
                        case "crimecu":
                        case "crimecj":
                        case "tetrist":
                        case "ashura":
                        case "ashuraj":
                        case "ashurau":
                        case "selfeena":
                            savestate_callback = Taitob.SaveStateBinary_nastar;
                            loadstate_callback = Taitob.LoadStateBinary_nastar;
                            break;
                        case "viofight":
                        case "viofightu":
                        case "viofightj":
                            savestate_callback = Taitob.SaveStateBinary_viofight;
                            loadstate_callback = Taitob.LoadStateBinary_viofight;
                            break;
                        case "hitice":
                        case "hiticerb":
                        case "hiticej":
                            savestate_callback = Taitob.SaveStateBinary_hitice;
                            loadstate_callback = Taitob.LoadStateBinary_hitice;
                            break;
                        case "silentd":
                        case "silentdj":
                        case "silentdu":
                            savestate_callback = Taitob.SaveStateBinary_silentd;
                            loadstate_callback = Taitob.LoadStateBinary_silentd;
                            break;
                        case "qzshowby":
                        case "pbobble":
                        case "spacedx":
                        case "spacedxj":
                            savestate_callback = Taitob.SaveStateBinary_silentd;
                            loadstate_callback = Taitob.LoadStateBinary_silentd;
                            break;
                    }
                    break;
                case "Konami 68000":
                    switch (Machine.sName)
                    {
                        case "cuebrick"://ym K052109 K051960
                            savestate_callback = Konami68000.SaveStateBinary_cuebrick;
                            loadstate_callback = Konami68000.LoadStateBinary_cuebrick;
                            break;
                        case "mia"://ym k007232 K052109 K051960
                            savestate_callback = Konami68000.SaveStateBinary_mia;
                            loadstate_callback = Konami68000.LoadStateBinary_mia;
                            break;
                        case "tmnt":
                        case "tmntu":
                        case "tmntua":
                        case "tmntub":
                        case "tmht":
                        case "tmhta":
                        case "tmhtb":
                        case "tmntj":
                        case "tmnta":
                        case "tmht2p":
                        case "tmht2pa":
                        case "tmnt2pj":
                        case "tmnt2po"://ym k007232 upd samples K052109 K051960
                            savestate_callback = Konami68000.SaveStateBinary_tmnt;
                            loadstate_callback = Konami68000.LoadStateBinary_tmnt;
                            break;
                        case "punkshot":
                        case "punkshot2":
                        case "punkshotj"://ym k053260 K052109 K051960
                            savestate_callback = Konami68000.SaveStateBinary_punkshot;
                            loadstate_callback = Konami68000.LoadStateBinary_punkshot;
                            break;                        
                        case "lgtnfght":
                        case "lgtnfghta":
                        case "lgtnfghtu":
                        case "trigon"://ym k053260 K052109 K053245
                            savestate_callback = Konami68000.SaveStateBinary_lgtnfght;
                            loadstate_callback = Konami68000.LoadStateBinary_lgtnfght;
                            break;
                        case "blswhstl":
                        case "blswhstla":
                        case "detatwin"://ym k053260 K052109 K053245 eeprom bytee
                            savestate_callback = Konami68000.SaveStateBinary_blswhstl;
                            loadstate_callback = Konami68000.LoadStateBinary_blswhstl;
                            break;
                        case "glfgreat":
                        case "glfgreatj"://k053260 K052109 K053245
                            savestate_callback = Konami68000.SaveStateBinary_glfgreat;
                            loadstate_callback = Konami68000.LoadStateBinary_glfgreat;
                            break;
                        case "tmnt2":
                        case "tmnt2a":
                        case "tmht22pe":
                        case "tmht24pe":
                        case "tmnt22pu":
                        case "qgakumon"://ym k053260 K052109 K053245 eeprom tmnt2_1c0800
                            savestate_callback = Konami68000.SaveStateBinary_tmnt2;
                            loadstate_callback = Konami68000.LoadStateBinary_tmnt2;
                            break;
                        case "ssriders":
                        case "ssriderseaa":
                        case "ssridersebd":
                        case "ssridersebc":
                        case "ssridersuda":
                        case "ssridersuac":
                        case "ssridersuab":
                        case "ssridersubc":
                        case "ssridersadd":
                        case "ssridersabd":
                        case "ssridersjad":
                        case "ssridersjac":
                        case "ssridersjbd"://ym k053260 K052109 K053245 eeprom
                            savestate_callback = Konami68000.SaveStateBinary_ssriders;
                            loadstate_callback = Konami68000.LoadStateBinary_ssriders;
                            break;                        
                        case "thndrx2":
                        case "thndrx2a":
                        case "thndrx2j"://ym k053260 K052109 K051960 eeprom
                            savestate_callback = Konami68000.SaveStateBinary_thndrx2;
                            loadstate_callback = Konami68000.LoadStateBinary_thndrx2;
                            break;
                        case "prmrsocr":
                        case "prmrsocrj"://k054539 K052109 K053245 eeprom
                            savestate_callback = Konami68000.SaveStateBinary_prmrsocr;
                            loadstate_callback = Konami68000.LoadStateBinary_prmrsocr;
                            break;
                    }
                    break;
                case "Capcom":
                    switch (Machine.sName)
                    {
                        case "gng":
                        case "gnga":
                        case "gngbl":
                        case "gngprot":
                        case "gngblita":
                        case "gngc":
                        case "gngt":
                        case "makaimur":
                        case "makaimurc":
                        case "makaimurg":
                        case "diamond":
                            savestate_callback = Capcom.SaveStateBinary_gng;
                            loadstate_callback = Capcom.LoadStateBinary_gng;
                            break;
                        case "sf":
                        case "sfua":
                        case "sfj":
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            savestate_callback = Capcom.SaveStateBinary_sf;
                            loadstate_callback = Capcom.LoadStateBinary_sf;
                            break;
                    }
                    break;
            }
        }
    }
}

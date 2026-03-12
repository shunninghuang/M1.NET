using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace mame
{
    public partial class Palette
    {
        public static uint[] entry_color, entry_color2;
        public static float[] group_bright, group_contrast, entry_contrast;
        private static uint trans_uint;
        private static int numcolors, numgroups;
        public static int[] dirty;
        public static int mindirty, maxdirty;
        public static Color trans_color;
        public delegate void palette_delegate(int index, uint rgb);
        public static palette_delegate palette_set_callback;
        public static void palette_init()
        {
            int i,index;
            numgroups = 1;
            group_bright = new float[3];
            group_contrast = new float[3];
            group_bright[0] = 0;
            group_bright[1] = 0;
            group_bright[2] = 0;
            group_contrast[0] = 1;
            group_contrast[1] = (float)0.6;
            group_contrast[2] = (float)(1 / 0.6);
            Video.video_attributes = 0;
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                case "CPS2":
                case "CPS2turbo":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0xc00;
                    palette_set_callback = palette_entry_set_color1;
                    break;
                case "Data East":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x200;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Tehkan":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x100;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Technos":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x180;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Tad":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x400;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Megasys1":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x400;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Gaelco":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x400;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "SunA8":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x100;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Namco System 1":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x2001;
                    palette_set_callback = palette_entry_set_color1;
                    break;
                case "IGS011":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x800;
                    palette_set_callback = palette_entry_set_color1;
                    break;
                case "PGM":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x901;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "M72":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x201;
                    palette_set_callback = palette_entry_set_color1;
                    break;
                case "M92":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x801;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Taito":
                    switch (Machine.sName)
                    {
                        case "tokio":
                        case "tokioo":
                        case "tokiou":
                        case "tokiob":
                        case "bublbobl":
                        case "bublbobl1":
                        case "bublboblr":
                        case "bublboblr1":
                        case "boblbobl":
                        case "sboblbobl":
                        case "sboblbobla":
                        case "sboblboblb":
                        case "sboblbobld":
                        case "sboblboblc":
                        case "bub68705":
                        case "dland":
                        case "bbredux":
                        case "bublboblb":
                        case "bublcave":
                        case "boblcave":
                        case "bublcave11":
                        case "bublcave10":
                            trans_color = Color.Magenta;
                            numcolors = 0x100;
                            break;
                        case "opwolf":
                        case "opwolfa":
                        case "opwolfj":
                        case "opwolfu":
                        case "opwolfb":
                        case "opwolfp":
                            trans_color = Color.Black;
                            numcolors = 0x2000;
                            break;
                    }
                    trans_uint = (uint)trans_color.ToArgb();
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Taito B":
                    trans_color = Color.Magenta;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x1000;
                    palette_set_callback = palette_entry_set_color2;
                    break;
                case "Konami 68000":
                    trans_color = Color.Black;
                    trans_uint = (uint)trans_color.ToArgb();
                    numcolors = 0x800;
                    numgroups = 3;
                    Video.video_attributes = 0x34;
                    switch (Machine.sName)
                    {
                        case "mia":
                        case "mia2":
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
                        case "tmnt2po":
                        case "thndrx2":
                        case "thndrx2a":
                        case "thndrx2j":
                            Video.video_attributes = 0x30;
                            break;
                    }
                    palette_set_callback = palette_entry_set_color4;
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
                            trans_color = Color.Black;
                            trans_uint = (uint)trans_color.ToArgb();
                            numcolors = 0x100;
                            palette_set_callback = palette_entry_set_color2;
                            break;
                        case "sf":
                        case "sfua":
                        case "sfj":
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            trans_color = Color.Black;
                            trans_uint = (uint)trans_color.ToArgb();
                            numcolors = 0x400;
                            palette_set_callback = palette_entry_set_color2;
                            break;
                    }
                    break;
            }
            entry_color = new uint[numcolors];
            entry_color2 = new uint[numcolors * numgroups];
            entry_contrast = new float[numcolors];
            allocate_palette();
            allocate_shadow_tables();
            for (index = 0; index < numcolors; index++)
            {
                entry_contrast[index] = (float)1.0;
                palette_set_callback(index, make_argb(0xff, pal1bit((byte)(index >> 0)), pal1bit((byte)(index >> 1)), pal1bit((byte)(index >> 2))));                
            }
            switch (Machine.sBoard)
            {
                case "SunA8":
                    entry_color[0xff] = trans_uint;
                    break;
                case "Namco System 1":
                    entry_color[0x2000] = trans_uint;
                    break;
                case "PGM":
                    entry_color[0x900] = trans_uint;
                    break;
                case "M72":
                    entry_color[0x200] = trans_uint;
                    break;
                case "M92":
                    entry_color[0x800] = trans_uint;
                    break;
                case "Taito":
                    switch (Machine.sName)
                    {
                        case "tokio":
                        case "tokioo":
                        case "tokiou":
                        case "tokiob":
                        case "bublbobl":
                        case "bublbobl1":
                        case "bublboblr":
                        case "bublboblr1":
                        case "boblbobl":
                        case "sboblbobl":
                        case "sboblbobla":
                        case "sboblboblb":
                        case "sboblbobld":
                        case "sboblboblc":
                        case "bub68705":
                        case "dland":
                        case "bbredux":
                        case "bublboblb":
                        case "bublcave":
                        case "boblcave":
                        case "bublcave11":
                        case "bublcave10":
                            entry_color[0xff] = trans_uint;
                            break;
                        case "opwolf":
                        case "opwolfa":
                        case "opwolfj":
                        case "opwolfu":
                        case "opwolfb":
                        case "opwolfp":
                            break;
                    }
                    break;
                case "Taito B":
                    entry_color[0] = trans_uint;
                    break;
                case "Konami 68000":
                    entry_color[0] = trans_uint;
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
                            //entry_color[0] = trans_uint;
                            break;
                        case "sf":
                        case "sfua":
                        case "sfj":
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            entry_color[0] = trans_uint;
                            break;
                    }
                    break;
            }
        }
        public static void palette_entry_set_color1(int index, uint rgb)
        {
            if (index >= numcolors || entry_color[index] == rgb)
            {
                return;
            }
            if (index % 0x10 == 0x0f && rgb == 0)
            {
                entry_color[index] = trans_uint;
            }
            else
            {
                entry_color[index] = 0xff000000 | rgb;
            }
        }
        public static void palette_entry_set_color2(int index, uint rgb)
        {
            if (index >= numcolors || entry_color[index] == rgb)
            {
                return;
            }
            entry_color[index] = 0xff000000 | rgb;
        }
        public static void palette_entry_set_color4(int index, uint rgb)
        {
            int groupnum;
            if (index >= numcolors || entry_color[index] == rgb)
            {
                return;
            }
            entry_color[index] = 0xff000000 | rgb;
            for (groupnum = 0; groupnum < numgroups; groupnum++)
            {
                update_adjusted_color(groupnum, index);
            }
        }
        public static void palette_entry_set_contrast(int index, float contrast)
        {
            int groupnum;
            if (index >= numcolors || entry_contrast[index] == contrast)
            {
                return;
            }
            entry_contrast[index] = contrast;
            for (groupnum = 0; groupnum < numgroups; groupnum++)
            {
                update_adjusted_color(groupnum, index);
            }
        }
        public static void palette_group_set_contrast(int group, float contrast)
        {
            int index;
            if (group >= numgroups || group_contrast[group] == contrast)
            {
                return;
            }
            group_contrast[group] = contrast;
            for (index = 0; index < numcolors; index++)
            {
                update_adjusted_color(group, index);
            }
        }
        public static uint make_rgb(int r, int g, int b)
        {
            return ((((uint)(r) & 0xff) << 16) | (((uint)(g) & 0xff) << 8) | ((uint)(b) & 0xff));
        }
        public static uint make_argb(int a, int r, int g, int b)
        {
            return ((((uint)(a) & 0xff) << 24) | (((uint)(r) & 0xff) << 16) | (((uint)(g) & 0xff) << 8) | ((uint)(b) & 0xff));
        }
        public static byte pal1bit(byte bits)
        {
            return (byte)(((bits & 1) != 0) ? 0xff : 0x00);
        }
        public static byte pal4bit(byte bits)
        {
            bits &= 0xf;
            return (byte)((bits << 4) | bits);
        }
        public static byte pal5bit(byte bits)
        {
            bits &= 0x1f;
            return (byte)((bits << 3) | (bits >> 2));
        }
        public static byte RGB_RED(uint rgb)
        {
            return (byte)((rgb >> 16) & 0xff);
        }
        public static byte RGB_GREEN(uint rgb)
        {
            return (byte)((rgb >> 8) & 0xff);
        }
        public static byte RGB_BLUE(uint rgb)
        {
            return (byte)(rgb & 0xff);
        }
        public static byte rgb_clamp(int value)
        {
            byte value2;
            if (value < 0)
            {
                value2 = 0;
            }
            if (value > 255)
            {
                value2 = 255;
            }
            else
            {
                value2 = (byte)value;
            }
            return value2;
        }
        public static uint adjust_palette_entry(uint entry, float brightness, float contrast)
        {
            int r = rgb_clamp((int)((float)RGB_RED(entry) * contrast + brightness));
            int g = rgb_clamp((int)((float)RGB_GREEN(entry) * contrast + brightness));
            int b = rgb_clamp((int)((float)RGB_BLUE(entry) * contrast + brightness));
            return make_argb(0xff,r, g, b);
        }
        public static void update_adjusted_color(int group, int index)
        {
            int finalindex = group * numcolors + index;
            uint adjusted;
            adjusted = adjust_palette_entry(entry_color[index], group_bright[group], group_contrast[group] * entry_contrast[index]);
            if (entry_color2[finalindex] == adjusted)
            {
                return;
            }
            entry_color2[finalindex] = adjusted;
        }
    }
}

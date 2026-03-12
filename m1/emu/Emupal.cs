using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Palette
    {
        public enum VIDEOATTRIBUTE
        {
            VIDEO_UPDATE_AFTER_VBLANK = 0x0004,
            VIDEO_HAS_SHADOWS = 0x0010,
            VIDEO_HAS_HIGHLIGHTS = 0x0020,
            VIDEO_BUFFERS_SPRITERAM = 0x40,
        }
        public static void palette_set_shadow_mode(int mode)
        {
            Drawgfx.imode = mode;
        }
        public static void allocate_palette()
        {
            if ((Video.video_attributes & (int)VIDEOATTRIBUTE.VIDEO_HAS_SHADOWS) != 0)
            {
                palette_group_set_contrast(1, (float)0.6);
            }
            if ((Video.video_attributes & (int)VIDEOATTRIBUTE.VIDEO_HAS_HIGHLIGHTS) != 0)
            {
                palette_group_set_contrast(2, (float)(1 / 0.6));
            }
        }
        public static void allocate_shadow_tables()
        {
            int[] table = new int[0x10000];
            int i;
            for (i = 0; i < 4; i++)
            {
                Drawgfx.shadow_table[i] = new int[0x10000];
            }
            if ((Video.video_attributes & (int)VIDEOATTRIBUTE.VIDEO_HAS_SHADOWS) != 0)
            {
                for (i = 0; i < 65536; i++)
                {
                    table[i] = (i < numcolors) ? (i + numcolors) : i;
                }
                Array.Copy(table, Drawgfx.shadow_table[0], 0x10000);
                Array.Copy(table, Drawgfx.shadow_table[2], 0x10000);
            }
            if ((Video.video_attributes & (int)VIDEOATTRIBUTE.VIDEO_HAS_HIGHLIGHTS) != 0)
            {
                for (i = 0; i < 65536; i++)
                {
                    table[i] = (i < numcolors) ? (i + 2 * numcolors) : i;
                }
                Array.Copy(table, Drawgfx.shadow_table[1], 0x10000);
                Array.Copy(table, Drawgfx.shadow_table[3], 0x10000);
            }
        }
        public static void palette_set_brightness(int pen, double bright)
        {
            palette_entry_set_contrast(pen, (float)bright);
        }
    }
}

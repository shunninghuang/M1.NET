using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Drawgfx
    {
        public static void common_drawgfx_pcktgal(byte[] bb1, int gfxwidth, int gfxheight, int gfxsrcmodulo, int gfxtotal_elements, int code, int color, int flipx, int flipy, int sx, int sy, RECT clip)
        {
            int ox;
            int oy;
            int ex;
            int ey;
            code %= gfxtotal_elements;
            ox = sx;
            oy = sy;
            ex = sx + gfxwidth - 1;
            if (sx < 0)
            {
                sx = 0;
            }
            if (sx < clip.min_x)
            {
                sx = clip.min_x;
            }
            if (ex >= 0x100)
            {
                ex = 0x100 - 1;
            }
            if (ex > clip.max_x)
            {
                ex = clip.max_x;
            }
            if (sx > ex)
            {
                return;
            }
            ey = sy + gfxheight - 1;
            if (sy < 0)
            {
                sy = 0;
            }
            if (sy < clip.min_y)
            {
                sy = clip.min_y;
            }
            if (ey >= 0x100)
            {
                ey = 0x100 - 1;
            }
            if (ey > clip.max_y)
            {
                ey = clip.max_y;
            }
            if (sy > ey)
            {
                return;
            }
            int sw = gfxwidth;
            int sh = gfxheight;
            int sm = gfxsrcmodulo;
            int ls = sx - ox;
            int ts = sy - oy;
            int dw = ex - sx + 1;
            int dh = ey - sy + 1;
            int colorbase = 4 * color;
            blockmove_8toN_transpen16_m72(bb1, code, sw, sh, sm, ls, ts, flipx, flipy, dw, dh, 0x100, colorbase, 0, sx, sy);
        }
    }
}

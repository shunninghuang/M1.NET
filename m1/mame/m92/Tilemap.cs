using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Tmap
    {
        public void tile_update_m92(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int tile_index,memindex;
            int tile, attrib, code;
            int pen_data_offset, palette_base;
            byte group, flags;
            memindex = logical_to_memory[logindex];
            tile_index = 2 * memindex + M92.pf_layer[user_data].vram_base;
            attrib = M92.m92_vram_data[tile_index + 1];
            tile = M92.m92_vram_data[tile_index] + ((attrib & 0x8000) << 1);
            code = tile % total_elements;
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * (attrib & 0x7f);
            if ((attrib & 0x100) != 0)
            {
                group = 2;
            }
            else if ((attrib & 0x80) != 0)
            {
                group = 1;
            }
            else
            {
                group = 0;
            }
            flags = (byte)(((attrib >> 9) & 3) ^ (attributes & 0x03));
            tileflags[logindex] = tile_draw(M92.gfx11rom, pen_data_offset, x0, y0, palette_base, 0, group, flags);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Megasys1
    {
        public static Tmap[] megasys1_tmap;
        public static Tmap[][][] megasys1_tilemap;
    }
    public partial class Tmap
    {
        public void tile_update_0(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code1, code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code1 = Megasys1.megasys1_scrollram[user_data][memindex / 4];
            code = (code1 & 0xfff) * Megasys1.megasys1_16x16_scroll_factor[user_data] + (memindex & 3);
            color = code1 >> (16 - Megasys1.megasys1_bits_per_color_code);
            pen_data_offset = code * 0x40;
            palette_base = 0x100 * user_data + 0x10 * color;
            tileflags[logindex] = tile_draw(Megasys1.scrollrom[user_data], pen_data_offset, x0, y0, palette_base, 0, 0, 0);
        }
        public void tile_update_1(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code1, code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code1 = Megasys1.megasys1_scrollram[user_data][memindex];
            code = (code1 & 0xfff) * Megasys1.megasys1_8x8_scroll_factor[user_data];
            color = code1 >> (16 - Megasys1.megasys1_bits_per_color_code);
            pen_data_offset = code * 0x40;
            palette_base = 0x100 * user_data + 0x10 * color;
            tileflags[logindex] = tile_draw(Megasys1.scrollrom[user_data], pen_data_offset, x0, y0, palette_base, 0, 0, 0);
        }
        public void tile_update_1_lomakai(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code1, code, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            code1 = Megasys1.megasys1_scrollram[user_data][memindex];
            code = (code1 & 0xfff) * Megasys1.megasys1_8x8_scroll_factor[user_data];
            color = code1 >> (16 - Megasys1.megasys1_bits_per_color_code);
            pen_data_offset = code * 0x40;
            palette_base = 0x100 * 2 + 0x10 * color;
            tileflags[logindex] = tile_draw(Megasys1.scrollrom[user_data], pen_data_offset, x0, y0, palette_base, 0, 0, 0);
        }
    }
}

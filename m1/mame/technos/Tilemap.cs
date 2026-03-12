using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mame
{
    public partial class Technos
    {
        public static Tmap bg_tilemap, fg_tilemap;        
        public static int background_scan(int col,int row)
        {
            return (col & 0x0f) + ((row & 0x0f) << 4) + ((col & 0x10) << 4) + ((row & 0x10) << 5);
        }
    }
    public partial class Tmap
    {
        public void tile_update_ddragon_bg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            byte flags;
            int memindex;
            int code, attr, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            attr = Technos.ddragon_bgvideoram[2 * memindex];
            code = (Technos.ddragon_bgvideoram[2 * memindex + 1] + ((attr & 0x07) << 8)) % total_elements;
            color = (attr >> 3) & 0x07;
            flags = (byte)(((attr & 0xc0) >> 6) & 3);
            pen_data_offset = code * 0x100;
            palette_base = 0x100 + 0x10 * color;
            tileflags[logindex] = tile_draw(Technos.gfx3rom, pen_data_offset, x0, y0, palette_base, 0, 0, flags);
        }
        public void tile_update_ddragon_fg(int logindex, int col, int row)
        {
            int x0 = tilewidth * col;
            int y0 = tileheight * row;
            int memindex;
            int code, attr, color;
            int pen_data_offset, palette_base;
            memindex = logical_to_memory[logindex];
            attr = Technos.ddragon_fgvideoram[2 * memindex];
            code = (Technos.ddragon_fgvideoram[2 * memindex + 1] + ((attr & 0x07) << 8)) % total_elements;
            color = attr >> 5;
            pen_data_offset = code * 0x40;
            palette_base = 0x10 * color;
            tileflags[logindex] = tile_draw(Technos.gfx1rom, pen_data_offset, x0, y0, palette_base, 0, 0, 0);
        }
        public void tilemap_draw_instance_capcom_ddragon(RECT cliprect, int xpos, int ypos)
        {
            int mincol, maxcol, logindex;
            int x1, y1, x2, y2;
            int y, nexty;
            int offsety1, offsety2;
            int i;
            x1 = Math.Max(xpos, cliprect.min_x);
            x2 = Math.Min(xpos + width, cliprect.max_x + 1);
            y1 = Math.Max(ypos, cliprect.min_y);
            y2 = Math.Min(ypos + height, cliprect.max_y + 1);
            if (x1 >= x2 || y1 >= y2)
                return;
            x1 -= xpos;
            y1 -= ypos;
            x2 -= xpos;
            y2 -= ypos;
            offsety1 = y1;
            mincol = x1 / tilewidth;
            maxcol = (x2 + tilewidth - 1) / tilewidth;
            y = y1;
            nexty = tileheight * (y1 / tileheight) + tileheight;
            nexty = Math.Min(nexty, y2);
            for (; ; )
            {
                int row = y / tileheight;
                trans_t prev_trans = trans_t.WHOLLY_TRANSPARENT;
                trans_t cur_trans;
                int x_start = x1;
                int column;
                for (column = mincol; column <= maxcol; column++)
                {
                    int x_end;
                    if (column == maxcol)
                    {
                        cur_trans = trans_t.WHOLLY_TRANSPARENT;
                    }
                    else
                    {
                        logindex = row * cols + column;
                        if (tileflags[logindex] == Tilemap.TILE_FLAG_DIRTY)
                        {
                            tile_update3(logindex, column, row);
                        }
                        if ((tileflags[logindex] & mask) != 0)
                        {
                            cur_trans = trans_t.MASKED;
                        }
                        else
                        {
                            cur_trans = ((flagsmap[offsety1, column * tilewidth] & mask) == value) ? trans_t.WHOLLY_OPAQUE : trans_t.WHOLLY_TRANSPARENT;
                        }
                    }
                    if (cur_trans == prev_trans)
                    {
                        continue;
                    }
                    x_end = column * tilewidth;
                    x_end = Math.Max(x_end, x1);
                    x_end = Math.Min(x_end, x2);
                    if (prev_trans != trans_t.WHOLLY_TRANSPARENT)
                    {
                        int cury;
                        offsety2 = offsety1;
                        if (prev_trans == trans_t.WHOLLY_OPAQUE)
                        {
                            for (cury = y; cury < nexty; cury++)
                            {
                                Array.Copy(pixmap, offsety2 * width + x_start, Video.bitmapbase[Video.curbitmap], (offsety2 + ypos) * 0x180 + xpos + x_start, x_end - x_start);
                                offsety2++;
                            }
                        }
                        else if (prev_trans == trans_t.MASKED)
                        {
                            for (cury = y; cury < nexty; cury++)
                            {
                                for (i = xpos + x_start; i < xpos + x_end; i++)
                                {
                                    if ((flagsmap[offsety2, i - xpos] & mask) == value)
                                    {
                                        Video.bitmapbase[Video.curbitmap][(offsety2 + ypos) * 0x180 + i] = pixmap[offsety2 * width + i - xpos];
                                    }
                                }
                                offsety2++;
                            }
                        }
                    }
                    x_start = x_end;
                    prev_trans = cur_trans;
                }
                if (nexty == y2)
                {
                    break;
                }
                offsety1 += (nexty - y);
                y = nexty;
                nexty += tileheight;
                nexty = Math.Min(nexty, y2);
            }
        }
    }
}

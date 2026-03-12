using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;
using cpu.z80;

namespace mame
{
    public partial class Megasys1
    {
        public static void SaveStateBinary_z(BinaryWriter writer)
        {
            int i,j;
            writer.Write(dsw1);
            writer.Write(dsw2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x10000);
            writer.Write(Memory.audioram, 0, 0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(megasys1_objectram[i]);
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    writer.Write(megasys1_scrollram[i][j]);
                }
            }
            writer.Write(megasys1_active_layers);
            writer.Write(megasys1_sprite_bank);
            writer.Write(megasys1_screen_flag);
            writer.Write(megasys1_sprite_flag);
            writer.Write(ip_latched);
            writer.Write(megasys1_bits_per_color_code);
            for(i=0;i<3;i++)
            {
                writer.Write(megasys1_scrollx[i]);
            }
            for(i=0;i<3;i++)
            {
                writer.Write(megasys1_scrolly[i]);
            }
            for(i=0;i<3;i++)
            {
                writer.Write(megasys1_scroll_flag[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            Z80A.zz1[0].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            AY8910.AA8910[0].SaveStateBinary(writer);
            YM2203.FF2203[0].SaveStateBinary(writer);
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(AY8910.AA8910[0].stream.sample_rate);
            writer.Write(AY8910.AA8910[0].stream.new_sample_rate);
            writer.Write(AY8910.AA8910[0].stream.gain);
            writer.Write(AY8910.AA8910[0].stream.output_sampindex);
            writer.Write(AY8910.AA8910[0].stream.output_base_sampindex);
            writer.Write(YM2203.FF2203[0].stream.output_sampindex);
            writer.Write(YM2203.FF2203[0].stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_z(BinaryReader reader)
        {
            int i,j;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x10000);
            Memory.audioram = reader.ReadBytes(0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                megasys1_objectram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    megasys1_scrollram[i][j] = reader.ReadUInt16();
                }
            }
            megasys1_active_layers = reader.ReadUInt16();
            megasys1_sprite_bank = reader.ReadUInt16();
            megasys1_screen_flag = reader.ReadUInt16();
            megasys1_sprite_flag = reader.ReadUInt16();
            ip_latched = reader.ReadUInt16();
            megasys1_bits_per_color_code = reader.ReadInt32();
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollx[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scrolly[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            Z80A.zz1[0].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            AY8910.AA8910[0].LoadStateBinary(reader);
            YM2203.FF2203[0].LoadStateBinary(reader);
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            AY8910.AA8910[0].stream.sample_rate = reader.ReadInt32();
            AY8910.AA8910[0].stream.new_sample_rate = reader.ReadInt32();
            AY8910.AA8910[0].stream.gain = reader.ReadInt32();
            AY8910.AA8910[0].stream.output_sampindex = reader.ReadInt32();
            AY8910.AA8910[0].stream.output_base_sampindex = reader.ReadInt32();
            YM2203.FF2203[0].stream.output_sampindex = reader.ReadInt32();
            YM2203.FF2203[0].stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_a(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dsw1);
            writer.Write(dsw2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x10000);
            writer.Write(Memory.audioram, 0, 0x20000);
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(megasys1_objectram[i]);
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    writer.Write(megasys1_scrollram[i][j]);
                }
            }
            writer.Write(megasys1_active_layers);
            writer.Write(megasys1_sprite_bank);
            writer.Write(megasys1_screen_flag);
            writer.Write(megasys1_sprite_flag);
            writer.Write(ip_latched);
            writer.Write(megasys1_bits_per_color_code);
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrollx[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrolly[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scroll_flag[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            MC68000.mm1[1].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            OKI6295.oo1[1].SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(OKI6295.oo1[i].OKI.stream.output_sampindex);
                writer.Write(OKI6295.oo1[i].OKI.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_a(BinaryReader reader)
        {
            int i, j;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x10000);
            Memory.audioram = reader.ReadBytes(0x20000);
            for (i = 0; i < 0x1000; i++)
            {
                megasys1_objectram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    megasys1_scrollram[i][j] = reader.ReadUInt16();
                }
            }
            megasys1_active_layers = reader.ReadUInt16();
            megasys1_sprite_bank = reader.ReadUInt16();
            megasys1_screen_flag = reader.ReadUInt16();
            megasys1_sprite_flag = reader.ReadUInt16();
            ip_latched = reader.ReadUInt16();
            megasys1_bits_per_color_code = reader.ReadInt32();
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollx[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scrolly[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            MC68000.mm1[1].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            OKI6295.oo1[1].LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                OKI6295.oo1[i].OKI.stream.output_sampindex = reader.ReadInt32();
                OKI6295.oo1[i].OKI.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_b(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dsw1);
            writer.Write(dsw2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x20000);
            writer.Write(Memory.audioram, 0, 0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(megasys1_objectram[i]);
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    writer.Write(megasys1_scrollram[i][j]);
                }
            }
            writer.Write(megasys1_active_layers);
            writer.Write(megasys1_sprite_bank);
            writer.Write(megasys1_screen_flag);
            writer.Write(megasys1_sprite_flag);
            writer.Write(ip_latched);
            writer.Write(megasys1_bits_per_color_code);
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrollx[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrolly[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scroll_flag[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            MC68000.mm1[1].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            OKI6295.oo1[1].SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(OKI6295.oo1[i].OKI.stream.output_sampindex);
                writer.Write(OKI6295.oo1[i].OKI.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_b(BinaryReader reader)
        {
            int i, j;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x20000);
            Memory.audioram = reader.ReadBytes(0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                megasys1_objectram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    megasys1_scrollram[i][j] = reader.ReadUInt16();
                }
            }
            megasys1_active_layers = reader.ReadUInt16();
            megasys1_sprite_bank = reader.ReadUInt16();
            megasys1_screen_flag = reader.ReadUInt16();
            megasys1_sprite_flag = reader.ReadUInt16();
            ip_latched = reader.ReadUInt16();
            megasys1_bits_per_color_code = reader.ReadInt32();
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollx[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scrolly[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            MC68000.mm1[1].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            OKI6295.oo1[1].LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                OKI6295.oo1[i].OKI.stream.output_sampindex = reader.ReadInt32();
                OKI6295.oo1[i].OKI.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_c(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dsw1);
            writer.Write(dsw2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x10000);
            writer.Write(Memory.audioram, 0, 0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(megasys1_objectram[i]);
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    writer.Write(megasys1_scrollram[i][j]);
                }
            }
            writer.Write(megasys1_active_layers);
            writer.Write(megasys1_sprite_bank);
            writer.Write(megasys1_screen_flag);
            writer.Write(megasys1_sprite_flag);
            writer.Write(ip_latched);
            writer.Write(megasys1_bits_per_color_code);
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrollx[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrolly[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scroll_flag[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            MC68000.mm1[1].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            OKI6295.oo1[1].SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(OKI6295.oo1[i].OKI.stream.output_sampindex);
                writer.Write(OKI6295.oo1[i].OKI.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_c(BinaryReader reader)
        {
            int i, j;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x10000);
            Memory.audioram = reader.ReadBytes(0x10000);
            for (i = 0; i < 0x1000; i++)
            {
                megasys1_objectram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    megasys1_scrollram[i][j] = reader.ReadUInt16();
                }
            }
            megasys1_active_layers = reader.ReadUInt16();
            megasys1_sprite_bank = reader.ReadUInt16();
            megasys1_screen_flag = reader.ReadUInt16();
            megasys1_sprite_flag = reader.ReadUInt16();
            ip_latched = reader.ReadUInt16();
            megasys1_bits_per_color_code = reader.ReadInt32();
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollx[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scrolly[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            MC68000.mm1[1].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            OKI6295.oo1[1].LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                OKI6295.oo1[i].OKI.stream.output_sampindex = reader.ReadInt32();
                OKI6295.oo1[i].OKI.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_d(BinaryWriter writer)
        {
            int i, j;
            writer.Write(dsw1);
            writer.Write(dsw2);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x10000);
            writer.Write(Memory.audioram, 0, 0x20000);
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(megasys1_objectram[i]);
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    writer.Write(megasys1_scrollram[i][j]);
                }
            }
            writer.Write(megasys1_active_layers);
            writer.Write(megasys1_sprite_bank);
            writer.Write(megasys1_screen_flag);
            writer.Write(megasys1_sprite_flag);
            writer.Write(ip_latched);
            writer.Write(megasys1_bits_per_color_code);
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrollx[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scrolly[i]);
            }
            for (i = 0; i < 3; i++)
            {
                writer.Write(megasys1_scroll_flag[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM2151.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            OKI6295.oo1[1].SaveStateBinary(writer);
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.latched_value[i]);
            }
            for (i = 0; i < 2; i++)
            {
                writer.Write(Sound.utempdata[i]);
            }
            writer.Write(Sound.ym2151stream.output_sampindex);
            writer.Write(Sound.ym2151stream.output_base_sampindex);
            for (i = 0; i < 2; i++)
            {
                writer.Write(OKI6295.oo1[i].OKI.stream.output_sampindex);
                writer.Write(OKI6295.oo1[i].OKI.stream.output_base_sampindex);
            }
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_d(BinaryReader reader)
        {
            int i, j;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x10000);
            Memory.audioram = reader.ReadBytes(0x20000);
            for (i = 0; i < 0x1000; i++)
            {
                megasys1_objectram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 0x2000; j++)
                {
                    megasys1_scrollram[i][j] = reader.ReadUInt16();
                }
            }
            megasys1_active_layers = reader.ReadUInt16();
            megasys1_sprite_bank = reader.ReadUInt16();
            megasys1_screen_flag = reader.ReadUInt16();
            megasys1_sprite_flag = reader.ReadUInt16();
            ip_latched = reader.ReadUInt16();
            megasys1_bits_per_color_code = reader.ReadInt32();
            for (i = 0; i < 3; i++)
            {
                megasys1_scrollx[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scrolly[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 3; i++)
            {
                megasys1_scroll_flag[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            MC68000.mm1[1].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM2151.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            OKI6295.oo1[1].LoadStateBinary(reader);
            for (i = 0; i < 2; i++)
            {
                Sound.latched_value[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 2; i++)
            {
                Sound.utempdata[i] = reader.ReadUInt16();
            }
            Sound.ym2151stream.output_sampindex = reader.ReadInt32();
            Sound.ym2151stream.output_base_sampindex = reader.ReadInt32();
            for (i = 0; i < 2; i++)
            {
                OKI6295.oo1[i].OKI.stream.output_sampindex = reader.ReadInt32();
                OKI6295.oo1[i].OKI.stream.output_base_sampindex = reader.ReadInt32();
            }
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}

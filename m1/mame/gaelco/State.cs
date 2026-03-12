using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using cpu.m68000;
using cpu.m6809;

namespace mame
{
    public partial class Gaelco
    {
        public static void SaveStateBinary_bigkarnk(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw1);
            writer.Write(dsw2);
            writer.Write(bytes);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x8000);
            writer.Write(Memory.audioram, 0, 0x800);
            for (i = 0; i < 4; i++)
            {
                writer.Write(gaelco_vregs[i]);
            }
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(gaelco_videoram[i]);
            }
            for (i = 0; i < 0x800; i++)
            {
                writer.Write(gaelco_spriteram[i]);
            }
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(gaelco_screen[i]);
            }
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Generic.paletteram16[i]);
            }
            MC68000.mm1[0].SaveStateBinary(writer);
            M6809.mm1[0].SaveStateBinary(writer);
            Cpuint.SaveStateBinary(writer);
            writer.Write(Timer.global_basetime.seconds);
            writer.Write(Timer.global_basetime.attoseconds);
            Video.SaveStateBinary(writer);
            writer.Write(Sound.last_update_second);
            Cpuexec.SaveStateBinary(writer);
            Timer.SaveStateBinary(writer);
            YM3812.SaveStateBinary(writer);
            OKI6295.oo1[0].SaveStateBinary(writer);
            writer.Write(Sound.latched_value[0]);
            writer.Write(Sound.utempdata[0]);
            writer.Write(Sound.ym3812stream.output_sampindex);
            writer.Write(Sound.ym3812stream.output_base_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_bigkarnk(BinaryReader reader)
        {
            int i;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            bytes = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x8000);
            Memory.audioram = reader.ReadBytes(0x800);
            for (i = 0; i < 4; i++)
            {
                gaelco_vregs[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x1000; i++)
            {
                gaelco_videoram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x800; i++)
            {
                gaelco_spriteram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x1000; i++)
            {
                gaelco_screen[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            M6809.mm1[0].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            YM3812.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            Sound.latched_value[0] = reader.ReadUInt16();
            Sound.utempdata[0] = reader.ReadUInt16();
            Sound.ym3812stream.output_sampindex = reader.ReadInt32();
            Sound.ym3812stream.output_base_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
        public static void SaveStateBinary_maniacsq(BinaryWriter writer)
        {
            int i;
            writer.Write(dsw1);
            writer.Write(dsw2);
            writer.Write(bytes);
            for (i = 0; i < 0x400; i++)
            {
                writer.Write(Palette.entry_color[i]);
            }
            writer.Write(Memory.mainram, 0, 0x10000);
            writer.Write(Memory.audioram, 0, 0x800);
            for (i = 0; i < 4; i++)
            {
                writer.Write(gaelco_vregs[i]);
            }
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(gaelco_videoram[i]);
            }
            for (i = 0; i < 0x800; i++)
            {
                writer.Write(gaelco_spriteram[i]);
            }
            for (i = 0; i < 0x1000; i++)
            {
                writer.Write(gaelco_screen[i]);
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
            OKI6295.oo1[0].SaveStateBinary(writer);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_sampindex);
            writer.Write(OKI6295.oo1[0].OKI.stream.output_base_sampindex);
            writer.Write(Sound.mixerstream.output_sampindex);
            writer.Write(Sound.mixerstream.output_base_sampindex);
        }
        public static void LoadStateBinary_maniacsq(BinaryReader reader)
        {
            int i;
            dsw1 = reader.ReadByte();
            dsw2 = reader.ReadByte();
            bytes = reader.ReadByte();
            for (i = 0; i < 0x400; i++)
            {
                Palette.entry_color[i] = reader.ReadUInt32();
            }
            Memory.mainram = reader.ReadBytes(0x10000);
            Memory.audioram = reader.ReadBytes(0x800);
            for (i = 0; i < 4; i++)
            {
                gaelco_vregs[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x1000; i++)
            {
                gaelco_videoram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x800; i++)
            {
                gaelco_spriteram[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x1000; i++)
            {
                gaelco_screen[i] = reader.ReadUInt16();
            }
            for (i = 0; i < 0x400; i++)
            {
                Generic.paletteram16[i] = reader.ReadUInt16();
            }
            MC68000.mm1[0].LoadStateBinary(reader);
            Cpuint.LoadStateBinary(reader);
            Timer.global_basetime.seconds = reader.ReadInt32();
            Timer.global_basetime.attoseconds = reader.ReadInt64();
            Video.LoadStateBinary(reader);
            Sound.last_update_second = reader.ReadInt32();
            Cpuexec.LoadStateBinary(reader);
            Timer.LoadStateBinary(reader);
            OKI6295.oo1[0].LoadStateBinary(reader);
            OKI6295.oo1[0].OKI.stream.output_sampindex = reader.ReadInt32();
            OKI6295.oo1[0].OKI.stream.output_base_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_sampindex = reader.ReadInt32();
            Sound.mixerstream.output_base_sampindex = reader.ReadInt32();
        }
    }
}

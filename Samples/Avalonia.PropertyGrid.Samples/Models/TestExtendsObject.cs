﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using PropertyModels.Collections;
using PropertyModels.ComponentModel;

namespace Avalonia.PropertyGrid.Samples.Models
{
    public struct SVector3
    {
        public float x, y, z;

        public override readonly string ToString() => string.Format("{0:0.0}, {1:0.0}, {2:0.0}", x, y, z);
    }

    public class TestExtendsObject : MiniReactiveObject
    {
        [Category("Struct")]
        public Vector3 vec3Object { get; set; } = new Vector3();

        [Category("Struct")]
        public SVector3 vec3Struct { get; set; }

        [Category("Struct")]
        public BindingList<SVector3> vec3BindingList { get; set; } =
        [
            new SVector3(){ x = 7.8f, y = 3.14f, z = 0.0f }
        ];

        [Category("SelectableList")]
        public SelectableList<CountryInfo> Countries { get; set; }

        [Category("Boolean")]
        public bool toggleAble { get; set; } = true;

        [Category("Boolean")]
        public bool disableAble { get; set; } = false;

        [Category("Boolean")]
        public bool? threeState { get; set; }

        [Category("Boolean")]
        [ReadOnly(true)]
        public bool readonlyBoolean { get; set; }

        [Category("Boolean")]
        public static bool readonlyBoolean2 => true;

        public TestExtendsObject()
        {
            List<CountryInfo> list = [];

            var assets = AssetLoader.GetAssets(new Uri($"avares://{GetType().Assembly.GetName().Name}/Assets/country-flags"), null);
            foreach (var asset in assets)
            {
                list.Add(new CountryInfo(asset));
            }

            Countries = new SelectableList<CountryInfo>(list, list.Find(x => x.Code == "cn") ?? list.FirstOrDefault()!);
        }
    }

    public class CountryInfo
    {
        public Media.IImage Flag { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public CountryInfo(Uri asset)
        {
            Name = Code = Path.GetFileNameWithoutExtension(asset.LocalPath);

            try
            {
                var regionInfo = new RegionInfo(Code);

                Name = regionInfo.DisplayName;
            }
            catch
            {
            }

            using var stream = AssetLoader.Open(asset);
            Flag = new Media.Imaging.Bitmap(stream);
        }

        public override string ToString() => Name;

        public override bool Equals(object? obj)
        {
            if (obj is CountryInfo ci)
            {
                return Code == ci.Code;
            }

            return false;
        }

        public override int GetHashCode() => Code.GetHashCode();
    }
}

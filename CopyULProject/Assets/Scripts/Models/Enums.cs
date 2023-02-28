using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace EY.Model.Enums
{
    public enum Level
    {
        [EnumMember(Value = "Level0")]
        Level0,
        [EnumMember(Value = "Level1")]
        Level1,
        [EnumMember(Value = "Level2")]
        Level2,
        [EnumMember(Value = "Level3")]
        Level3,
        [EnumMember(Value = "Level4")]
        Level4,
        [EnumMember(Value = "None")]
        None
    }
}

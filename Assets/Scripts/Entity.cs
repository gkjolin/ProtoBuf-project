/*
-------------------------------------------------------
 Developer:  Alexander - twitter.com/wobes_1
 Date:       02/12/2017 10:55
-------------------------------------------------------
*/

using ProtoBuf;
using System;

public enum Gender
{
    Male,
    Female,
}

[Serializable] // Binary
[ProtoContract(SkipConstructor = true)]
public class Entity
{
    [ProtoMember(1)]
    public int ID { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public byte Age { get; set; }

    [ProtoMember(4)]
    public DateTime BirthDate { get; set; }

    [ProtoMember(5)]
    public Gender gender { get; set; }
}
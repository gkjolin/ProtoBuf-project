/*
-------------------------------------------------------
 Developer:  Alexander - twitter.com/wobes_1
 Date:       02/12/2017 10:54
-------------------------------------------------------
*/

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializeExample : MonoBehaviour
{
    private void Start()
    {
        var entities = new List<Entity>
        {
            new Entity
            {
                ID = 1,
                Name = "John",
                 Age = 31,
                 BirthDate = DateTime.Now,
                 gender = Gender.Male
            },
            new Entity
            {
                ID = 2,
                Name = "Emma",
                Age = 25,
                BirthDate = DateTime.Now,
                gender = Gender.Female
            }
        };

        // Binary

        UnityEngine.Debug.Log("The test of binary formatter:");

        string file1 = (Application.dataPath + "/Output/tasks1.bin");

        TestBinaryFormatter(entities, file1, 1000);
        TestBinaryFormatter(entities, file1, 2000);
        TestBinaryFormatter(entities, file1, 3000);
        TestBinaryFormatter(entities, file1, 4000);
        TestBinaryFormatter(entities, file1, 5000);

        // Protobuf
        // We have to prepare the ProtoBuf Serializer first.
        Serializer.PrepareSerializer<Entity>();
        Serializer.PrepareSerializer<List<Entity>>();

        UnityEngine.Debug.Log("The test of protobuf-net:");

        string file2 = (Application.dataPath + "/Output/tasks2.bin");

        TestProtoBuf(entities, file2, 1000);
        TestProtoBuf(entities, file2, 2000);
        TestProtoBuf(entities, file2, 3000);
        TestProtoBuf(entities, file2, 4000);
        TestProtoBuf(entities, file2, 5000);


        UnityEngine.Debug.Log("The comparison of file size:");

        UnityEngine.Debug.Log(string.Format("The size of <color=green>BinaryFormatter</color> file {0} is <color=red>{1}</color> bytes", file1, (new FileInfo(file1)).Length));
        UnityEngine.Debug.Log(string.Format("The size of <color=green>Protobuf</color> file {0} is <color=green>{1}</color> bytes", file2, (new FileInfo(file2)).Length));
    }

    private static void TestBinaryFormatter(IList<Entity> tasks, string fileName, int iterationCount)
    {
        var stopwatch = new Stopwatch();
        var formatter = new BinaryFormatter();

        using (var file = File.Create(fileName))
        {
            stopwatch.Start();

            for (var i = 0; i < iterationCount; i++)
            {
                file.Position = 0;
                formatter.Serialize(file, tasks);
                file.Position = 0;
                var restoredTasks = (List<Entity>)formatter.Deserialize(file);
            }

            stopwatch.Stop();

            UnityEngine.Debug.Log(string.Format("{0} iterations in <color=red>{1}</color> ms", iterationCount, stopwatch.ElapsedMilliseconds));
        }
    }

    private static void TestProtoBuf(IList<Entity> tasks, string fileName, int iterationCount)
    {
        var stopwatch = new Stopwatch();
        using (var file = File.Create(fileName))
        {
            stopwatch.Start();

            for (var i = 0; i < iterationCount; i++)
            {
                file.Position = 0;
                Serializer.Serialize(file, tasks);
                file.Position = 0;
                var restoredTasks = Serializer.Deserialize<List<Entity>>(file);
            }

            stopwatch.Stop();

            UnityEngine.Debug.Log(string.Format("{0} iterations in <color=green>{1}</color> ms", iterationCount, stopwatch.ElapsedMilliseconds));
        }
    }
}

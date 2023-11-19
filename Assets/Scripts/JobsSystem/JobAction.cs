using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobsSystem
{
    public class JobAction : MonoBehaviour
    {
        private void Start()
        {
            NativeArray<int> dataArray = new NativeArray<int>(10, Allocator.TempJob);

            for (int i = 0; i < dataArray.Length; i++)
            {
                dataArray[i] = Random.Range(0, 20);
            }

            JobStruct job = new JobStruct
            {
                data = dataArray
            };

            JobHandle jobHandle = job.Schedule();
            jobHandle.Complete();

            for (int i = 0; i < dataArray.Length; i++)
            {
                Debug.Log("TASK 1: Value at index " + i + ": " + dataArray[i]);
            }

            dataArray.Dispose();
        }
    }
}
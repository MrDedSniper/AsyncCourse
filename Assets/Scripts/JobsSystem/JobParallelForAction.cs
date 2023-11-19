using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobsSystem
{
    public class JobParallelForAction : MonoBehaviour
    {
        private void Start()
        {
            NativeArray<Vector3> positionsArray = new NativeArray<Vector3>(10, Allocator.TempJob);
            NativeArray<Vector3> velocitiesArray = new NativeArray<Vector3>(10, Allocator.TempJob);
            NativeArray<Vector3> finalPositionsArray = new NativeArray<Vector3>(10, Allocator.TempJob);

            for (int i = 0; i < positionsArray.Length; i++)
            {
                positionsArray[i] = new Vector3(i, i, i);
                velocitiesArray[i] = new Vector3(1, 1, 1);
            }

            JobParallelFor job = new JobParallelFor
            {
                Positions = positionsArray,
                Velocities = velocitiesArray,
                FinalPositions = finalPositionsArray
            };

            JobHandle jobHandle = job.Schedule(positionsArray.Length, 64);
            jobHandle.Complete();

            for (int i = 0; i < finalPositionsArray.Length; i++)
            {
                Debug.Log("TASK 2: Final position at index " + i + ": " + finalPositionsArray[i]);
            }

            positionsArray.Dispose();
            velocitiesArray.Dispose();
            finalPositionsArray.Dispose();
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

internal class Task2Script : MonoBehaviour
{
    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
    [SerializeField] internal bool _testingTask3;
    
    private void Start()
    {
        CancellationToken cancelToken = cancelTokenSource.Token;

        Task task1 = Task1(cancelToken);
        Task task2 = Task2(cancelToken);
        
        if (_testingTask3)
        {
            WhatTaskFasterAsync(cancelToken, task1, task2);
        }
    }

    internal async Task<int> Task1(CancellationToken token)
    {
        int millisecondsDelay = 1000;

        await Task.Delay(millisecondsDelay,token);

        Debug.Log($"Task 1 completed");
        return millisecondsDelay;
    }

    internal async Task<int> Task2(CancellationToken token)
    {
        int frames = 0;

        while (frames < 60)
        {
            await Task.Yield();
            frames++;
            if (token.IsCancellationRequested) break;
        }
        Debug.Log($"Task 2 completed");
        return frames;
    }
    
    private async Task<bool> WhatTaskFasterAsync(CancellationToken token, Task firstTask, Task secondTask)
    {
        bool result = false;
        await Task.Factory.StartNew(async _ => {
            Task completedTask = await Task.WhenAny(firstTask, secondTask);
            if(completedTask == firstTask)
            {
                result = true;
                secondTask.Wait(token);
            }
            else
            {
                result = false;
                firstTask.Wait(token);
            }
        }, token); 
        return result;
    }
    private void OnDestroy()
    {
        cancelTokenSource.Cancel();
        cancelTokenSource.Dispose();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

internal class Task3Script : MonoBehaviour
{
    [SerializeField] private Task2Script _task2Script;
    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
    [SerializeField] internal bool _testingTask3;
    
    private void Start()
    {
        CancellationToken cancelToken = cancelTokenSource.Token;

        Task task1 = _task2Script.Task1(cancelToken);
        Task task2 = _task2Script.Task2(cancelToken);
        
        if (_testingTask3)
        {
            WhatTaskFasterAsync(cancelToken, task1, task2);
        }
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
}

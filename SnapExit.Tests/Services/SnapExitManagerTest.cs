﻿using SnapExit.Interfaces;
using SnapExit.Services;
using SnapExit.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapExit.Tests.Services
{
    class SnapExitManagerTest : SnapExitManager<SnapExitReponse, object>
    {
        private readonly IExecutionControlService _executionControlService;

        // use to Assert in the unit test
        public SnapExitReponse? response;
        public object? environment;

        public SnapExitManagerTest(IExecutionControlService executionControlService) : base(executionControlService)
        {
            _executionControlService = executionControlService;
        }

        public async Task SetupSnapExit(SnapExitReponse response)
        {
            _executionControlService.EnvironmentData = new { };
            await RegisterSnapExitAsync(SomeLongTask(response));
        }

        private async Task SomeLongTask(SnapExitReponse response)
        {
            await _executionControlService.StopExecution(response);
        }

        protected override void SnapExitResponse(object? sender, OnSnapExitEventArgs args)
        {
            response = args.ResponseData;
            environment = args.EnvironmentData;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CamundaClient;
using CamundaClient.Dto;
using digitek.brannProsjektering.Models;
using digitek.brannProsjektering.Worker;
using FluentAssertions;

namespace digitek.brannProsjektering.Tests
{
    public class BpmnTestServices
    {
        public static string CreateTestName(string modelName)
        {
            var name = modelName.Substring(0, modelName.IndexOf("."));
            var testName = name + "_Testcase";
            return testName;
        }
        public static void UpdateExternalTaskWithTheResult(ExternalTask externalTask, Dictionary<string, object> externalTaskDictionary)
        {
            externalTask.Variables.Add(externalTaskDictionary.FirstOrDefault().Key,
                new Variable() { Value = externalTaskDictionary.FirstOrDefault().Value });
        }
        private static Dictionary<string, object> CheckOutputAndUpdateExternalTask(Dictionary<string, object> externalTaskOutputDictionary, string key)
        {
            var dictionaryByKey = externalTaskOutputDictionary.Where(value => value.Key.Contains(key))
                .ToDictionary(value => value.Key, value => value.Value);
            return dictionaryByKey;
        }
        public static Dictionary<string, object> CreateDictionaryFromModel(object brannklasseModel)
        {
            var dictionary = brannklasseModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(brannklasseModel, null));
            return dictionary;
        }
        public static void CheckExternalTask(CamundaEngineClient camunda, string TestName, string TopicName, string activityId, ref Dictionary<string, object> dictionary)
        {
            //Check that External Task is there
            var externalTasks = camunda.ExternalTaskService.FetchAndLockTasks(TestName, 100, TopicName, 1000, new List<string>());
            externalTasks.Count.Should().Be(1);
            externalTasks.First().ActivityId.Should().Be(activityId);
            camunda.ExternalTaskService.Complete(TestName, externalTasks.First().Id, dictionary);
        }


        public static Assembly GetAssembly(string assamblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(assamblyName)).ToArray();
            var assembly = (Assembly)assemblies.FirstOrDefault();
            assembly.Should().NotBeNull();
            return assembly;
        }

        public static CamundaEngineClient CamundaEngineClient()
        {
            string camundaUrl = "http://localhost:8080/engine-rest/engine/default/";
            var camunda = new CamundaEngineClient(new System.Uri(camundaUrl), null, null);
            camunda.Should().NotBeNull();
            return camunda;
        }

        public static string DeployModelToCamunda(CamundaEngineClient camunda, string modelPath, Assembly assembly, string testName)
        {
            var deploymentId = camunda.RepositoryService.Deploy(testName,
                 new List<object>
                 {
                    FileParameter.FromManifestResource(assembly, modelPath)
                 });
            //check that can start the sub model
            deploymentId.Should().NotBeNullOrEmpty();
            return deploymentId;

        }
        public static string StartInstance(CamundaEngineClient camunda, string processDefinitionKey, Dictionary<string, object> dictionary)
        {
            //check porcess is started
            var processInstanceId = camunda.BpmnWorkflowService.StartProcessInstance(processDefinitionKey, dictionary);
            processInstanceId.Should().NotBeNullOrEmpty();
            return processInstanceId;
        }


        public static Dictionary<string, Variable> GetProcessVariables(CamundaEngineClient camunda, string processInstanceId)
        {
            var processVariables = camunda.BpmnWorkflowService.GetProcessVariables(processInstanceId);
            var variables = CamundaClientHelper.ConvertVariables(processVariables);
            variables.Should().NotBeNull();
            return variables;
        }
        public static Dictionary<string, object> GetModelInputsExternalTaskOutput(ExternalTask externalTask)
        {
            var modelVariables = new Dictionary<string, object>();
            new InputsValidationWorker().Execute(externalTask, ref modelVariables);
            var brannInputsValidationExternalTasks = CheckOutputAndUpdateExternalTask(modelVariables, "modelInputs");
            brannInputsValidationExternalTasks.Count().Should().Be(1);
            return brannInputsValidationExternalTasks;
        }

        public static Dictionary<string, object> GetOutputConsolidationExternalTaskOutput(ExternalTask externalTask)
        {
            var modelVariables = new Dictionary<string, object>();
            new OutputConsolidation().Execute(externalTask, ref modelVariables);
            var outputConsolidationExternalTasks = CheckOutputAndUpdateExternalTask(modelVariables, "modelOutputs");
            outputConsolidationExternalTasks.Count().Should().Be(1);
            return outputConsolidationExternalTasks;
        }

        public static Dictionary<string, object> GetModelOutputsDataDictionaryExternalTaskOutput(ExternalTask externalTask)
        {

            var modelVariables = new Dictionary<string, object>();
            new ModelOutputsDataDictionary().Execute(externalTask, ref modelVariables);
            var getModelOutputsDataDictionaryExternalTaskOutput = CheckOutputAndUpdateExternalTask(modelVariables, "modelDataDictionary");
            getModelOutputsDataDictionaryExternalTaskOutput.Count().Should().Be(1);
            return getModelOutputsDataDictionaryExternalTaskOutput;
        }
    }
}

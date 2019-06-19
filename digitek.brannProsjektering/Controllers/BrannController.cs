using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CamundaClient;
using digitek.brannProsjektering.Models;
using CamundaClient.Requests;
using CamundaClient.Service;
using digitek.brannProsjektering.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace digitek.brannProsjektering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigiTek17K11Controller : ControllerBase
    {
        private readonly ICamundaEngineClient _camundaClient;
        private readonly IDbServices _dbServices;
        private Dictionary<string, object> _processVariables;

        public DigiTek17K11Controller(ICamundaEngineClient camundaClient, IDbServices dbServices)
        {
            _camundaClient = camundaClient;
            _dbServices = dbServices;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branntekniskProsjekteringModel"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("BranntekniskProsjekteringModel")]
        [Produces("application/json", Type = typeof(BranntekniskProsjekteringObject))]
        [Consumes("application/Json")]
        public IActionResult Postbrannpro([FromBody] BranntekniskProsjekteringObject branntekniskProsjekteringObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "BranntekniskProsjekteringModel";
            var dictionary = ModelToDictionary(branntekniskProsjekteringObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, branntekniskProsjekteringObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(branntekniskProsjekteringObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="risikoklasseObject"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("RisikoklasseSubModel")]
        [Produces("application/json", Type = typeof(RisikoklasseObject))]
        [Consumes("application/Json")]
        public IActionResult PostRisikoklasseSubModel([FromBody] RisikoklasseObject risikoklasseObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "RisikoklasseSubModel";
            var dictionary = ModelToDictionary(risikoklasseObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, risikoklasseObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(risikoklasseObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brannklasseObject"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("BrannklasseSubModel")]
        [Produces("application/json", Type = typeof(BrannklasseObject))]
        [Consumes("application/Json")]
        public IActionResult PostBrannklasseModel([FromBody] BrannklasseObject brannklasseObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "BrannklasseSubModel";
            var dictionary = ModelToDictionary(brannklasseObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, brannklasseObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(brannklasseObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kravTilBranntiltakObject"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("KravTilBranntiltakSubModel")]
        [Produces("application/json", Type = typeof(KravTilBranntiltakObject))]
        [Consumes("application/Json")]
        public IActionResult PostKravTilBranntiltakSubModel([FromBody] KravTilBranntiltakObject kravTilBranntiltakObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "KravTilBranntiltakSubModel";
            var dictionary = ModelToDictionary(kravTilBranntiltakObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, kravTilBranntiltakObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(kravTilBranntiltakObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brannseksjonOgBrannmotstandObject"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("BrannseksjonOgBrannmotstandSubModel")]
        [Produces("application/json", Type = typeof(BrannseksjonOgBrannmotstandObject))]
        [Consumes("application/Json")]
        public IActionResult PostBrannseksjonOgBrannmotstandSubModel([FromBody] BrannseksjonOgBrannmotstandObject brannseksjonOgBrannmotstandObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "BrannseksjonOgBrannmotstandSubModel";
            var dictionary = ModelToDictionary(brannseksjonOgBrannmotstandObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, brannseksjonOgBrannmotstandObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(brannseksjonOgBrannmotstandObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="brannmotstandObject"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpPost, Route("BrannmotstandSubModel")]
        [Produces("application/json", Type = typeof(BrannmotstandObject))]
        [Consumes("application/Json")]
        public IActionResult PostBrannmotstandSubModel([FromBody] BrannmotstandObject brannmotstandObject, bool? justValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = "BrannmotstandSubModel";
            var dictionary = ModelToDictionary(brannmotstandObject.ModelInputs);

            // Start proces in camunda Server and get executionId
            var responce = _camundaClient.BpmnWorkflowService.StartProcessInstance(key, dictionary);

            // generate response
            var actionResponse = ActionResultResponse(justValues, responce, brannmotstandObject.UserInfo);

            // create User Record
            var useRecord = CreateUseRedordModel(brannmotstandObject, responce, key, "11");

            // Add user recor to DB
            try
            {
                _dbServices.AddUseRecord(useRecord);
            }
            catch
            {
                return StatusCode(503, "Cant save use record to Data Base");
            }

            return actionResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="justValues"></param>
        /// <returns></returns>
        [HttpGet("GetResult/{id}", Name = "Get")]
        public IActionResult Get(string id, bool? justValues)
        {
            var responce = _camundaClient.BpmnWorkflowService.GetProcessVariables(id);
            if (responce != null && responce.Any())
            {
                if (justValues.HasValue && justValues.Value)
                {
                    var newDict = responce.Where(value => value.Key.Contains("modelOutputs"))
                        .ToDictionary(value => value.Key, value => value.Value);
                    return Ok(newDict);
                }

                return Ok(responce);
            }
            return BadRequest(responce);
        }
        private IActionResult ActionResultResponse(bool? justValues, string responce, UserInfo userInfo)
        {
            //Check if the process could be start
            if (!ResponseIsExecutionId(responce))
                return BadRequest("Bad request");

            // Get response variables from Model
            _processVariables = GetVariables(responce, justValues, userInfo);
            return Ok(_processVariables);
        }
        private Dictionary<string, object> ModelToDictionary(object model)
        {
            Dictionary<string, object> modelDictionary = model?.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(model, null));
            return modelDictionary;
        }

        private bool ResponseIsExecutionId(string responce)
        {
            return Guid.TryParse(responce, out var guid);
        }

        private Dictionary<string, object> GetVariables(string executionId, bool? justValues, UserInfo userInfo)
        {
            Dictionary<string, object> processVariables = null;

            //Loop to retry if camunda server is not answering
            for (int i = 0; i < 3; i++)
            {
                processVariables = _camundaClient.BpmnWorkflowService.GetProcessVariables(executionId);

                //Unswer from Camunda, for ex if is BKL4
                if (processVariables.ContainsKey("Advarsel"))
                    break;
                //Bud request
                if (processVariables.ContainsKey("Error"))
                {
                    processVariables.Add("Error", "Could not load result variables");
                    break;
                }

                if (processVariables.ContainsKey("modelOutputs"))
                {
                    if (justValues.HasValue && justValues.Value)
                    {
                        var newDict = processVariables.Where(value => value.Key.Contains("modelOutputs"))
                            .ToDictionary(value => value.Key, value => value.Value);
                        processVariables = newDict;
                    }
                    break;
                }
                Task.Delay(500);
            }

            processVariables?.Add("ExecutionInfo", new Dictionary<string, string>()
            {
                {"ExecutionId",executionId },
                {"Navn",userInfo.Navn },
                {"e-post",userInfo.Email },
                {"OrgNr",userInfo.Organisasjonsnummer },
                {"OrgNavn",userInfo.OrganisasjonsNavn },
                {"Dato",DateTime.Now.ToString("dd.MM.yyyy HH:mm") },
            });
            return processVariables;
        }

        private UseRecord CreateUseRedordModel(object branntekniskProsjekteringModel, string responce, string model, string tekChapter)
        {
            var useRecord = new UseRecord();
            //add Model 
            useRecord.Model = model;
            useRecord.Kapitel = tekChapter;

            // Add ExecutionId
            useRecord.ExecutionNr = ResponseIsExecutionId(responce) ? responce : null;

            //serialize Object
            var json = JsonConvert.SerializeObject(branntekniskProsjekteringModel);

            //replace String text from swagger to null
            var newJson = json.Replace("string", null);
            var jsonObj = JObject.Parse(newJson);

            //Add User Info
            var userInfo = jsonObj["UserInfo"];

            AddUserInfo(userInfo, ref useRecord);

            //Add date&Time
            useRecord.DateTime = DateTime.Now;

            // add Json input
            var modelInputs = JsonConvert.SerializeObject(jsonObj["ModelInputs"]);
            if (!string.IsNullOrEmpty(modelInputs))
                useRecord.InputJson = modelInputs;

            if (_processVariables != null)
            {
                useRecord.ResponseCode = 200;
                useRecord.ResponseText = JsonConvert.SerializeObject(_processVariables.ContainsKey("modelOutputs") ? _processVariables["modelOutputs"] : _processVariables);

            }
            else
            {
                var responceArrey = responce.Split("-", 2);
                if (int.TryParse(responceArrey[0], out var code))
                {
                    useRecord.ResponseCode = code;
                    useRecord.ResponseText = responceArrey[1];
                }
                else
                {
                    useRecord.ResponseText = responce;
                }
            }

            return useRecord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAvailablesModels")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetAvailablesModels()
        {
            var bpmnModels = GetBmpnAvelabalsModelsType();

            var bpmnInformationList = new List<bpmnInformationModel>();
            foreach (var bpmnModel in bpmnModels)
            {
                Dictionary<string, string> modelProperties = GetModelPropertiesNameAndType(bpmnModel.Value);
                bpmnInformationList.Add(new bpmnInformationModel()
                {
                    BpmnName = bpmnModel.Key,
                    BpmnInpust = modelProperties
                });

            }


            return Ok(bpmnInformationList);
        }

        public static Dictionary<string, object> GetBmpnAvelabalsModelsType()
        {
            var bmpnAvelabalsModels = new Dictionary<string, object>();
            foreach (BpmnModels bpmnModel in Enum.GetValues(typeof(BpmnModels)))
            {
                var bpmnModelName = bpmnModel.ToString();
                switch (bpmnModel)
                {
                    case BpmnModels.BranntekniskProsjekteringModel:
                        bmpnAvelabalsModels.Add(bpmnModelName,new BranntekniskProsjekteringModel());
                        break;
                    case BpmnModels.BrannklasseSubModel:
                        bmpnAvelabalsModels.Add(bpmnModelName, new BrannklasseModel());
                        break;
                    case BpmnModels.BrannmotstandSubModel:
                        bmpnAvelabalsModels.Add(bpmnModelName, new BrannmotstandModel());
                        break;
                    case BpmnModels.BrannseksjonOgBrannmotstandSubModel:
                        bmpnAvelabalsModels.Add(bpmnModelName, new BrannseksjonOgBrannmotstandModel());

                        break;
                    case BpmnModels.KravTilBranntiltakSubModel:
                        bmpnAvelabalsModels.Add(bpmnModelName, new KravTilBranntiltakModel());
                        break;
                    case BpmnModels.RisikoklasseSubModel:
                        bmpnAvelabalsModels.Add(bpmnModelName, new RisikoklasseModel());

                        break;
                }
            }

            return bmpnAvelabalsModels;
        }

        public static Dictionary<string, string> GetModelPropertiesNameAndType(object model)
        {
            var modelPropertiesDictionary = new Dictionary<string, string>();
            var modelProperties = model.GetType().GetProperties();

            if (modelProperties != null)
            {
                foreach (var property in modelProperties)
                {
                    //GET general type for nullable variables
                    var modelGenericType = property.PropertyType.GenericTypeArguments;

                    string propertyTypeName = modelGenericType.Any() ? modelGenericType?.First().Name : property.PropertyType.Name;
                    modelPropertiesDictionary.Add(property.Name, propertyTypeName);
                }
            }


            return modelPropertiesDictionary;
        }


        private void AddUserInfo(JToken userInfo, ref UseRecord useRecord)
        {
            if (userInfo.Type == JTokenType.Null)
            {
                return;
            }
            var user = userInfo.ToObject<UserInfo>();
            if (!ObjectPropertiesIsNullOrEmpty(user))
            {
                if (!string.IsNullOrEmpty(userInfo["Navn"].ToString()))
                    useRecord.Navn = userInfo["Navn"].ToString();
                if (!string.IsNullOrEmpty(userInfo["Organisasjonsnummer"].ToString()))
                    useRecord.Organisasjonsnummer = userInfo["Organisasjonsnummer"].ToString();
                if (!string.IsNullOrEmpty(userInfo["Email"].ToString()))
                    useRecord.Email = userInfo["Email"].ToString();
            }
        }
        private static bool ObjectPropertiesIsNullOrEmpty(object obj)
        {
            var propertiesValues = obj.GetType().GetProperties()
                .Select(prop => prop.GetValue(obj, null))
                .Where(val => val != null)
                .Select(val => val.ToString())
                .Where(str => str.Length > 0)
                .Where(v => v.ToLower() != "string" && v != "0")
                .ToList();

            return !propertiesValues.Any();
        }

        private enum BpmnModels
        {
            BranntekniskProsjekteringModel,
            RisikoklasseSubModel,
            BrannklasseSubModel,
            KravTilBranntiltakSubModel,
            BrannseksjonOgBrannmotstandSubModel,
            BrannmotstandSubModel,
        }
    }
}

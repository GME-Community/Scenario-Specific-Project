

var gmesdk = {
	QAVSDK_Poll: function (){
		return 1006;
	},

	QAVSDK_GetSDKVersion: function (){
		return 1006;
	},

	QAVSDK_SetAppVersion: function (sAppVersion){
		return 1006;
	},

	QAVSDK_AVContext_CheckMicPermission: function (){
		return 1006;
	},

	QAVSDK_AVContext_SetLogLevel: function (levelWrite, levelPrint){
		return 1006;
	},

	QAVSDK_AVContext_SetLogPath: function (logDir){
		return 1006;
	},

	QAVSDK_AVContext_IsContextStarted: function (){
		return 1006;
	},

	QAVSDK_AVContext_SetRegion: function (region){
		return 1006;
	},

	QAVSDK_AVContext_SetHost: function (chatHost, pttHost){
		return 1006;
	},

	QAVSDK_AVContext_Stop: function (){
		return 1006;
	},

	QAVSDK_AVContext_Destroy: function (){
		return 1006;
	},

	QAVSDK_AVContext_CheckMic: function (){
		return 1006;
	},

	QAVSDK_AVContext_IsRoomEntered: function (){
		return 1006;
	},

	QAVSDK_AVContext_ExitRoom: function (){
	    gmeAPI.ExitRoom();
		return 0;
	},

	QAVSDK_AVContext_SetRecvMixStreamCount: function (nMixCount){
		return 1006;
	},

	QAVSDK_AVContext_Resume: function (){
		return 1006;
	},

	QAVSDK_AVContext_Pause: function (){
		return 1006;
	},

	QAVSDK_AVContext_SetAdvanceParams: function (KeyCode, value){
		return 1006;
	},

	QAVSDK_AVContext_StartRealTimeASR: function (language){
		return 1006;
	},

	QAVSDK_AVContext_StopRealTimeASR: function (){
		return 1006;
	},

	QAVSDK_AVContext_InitFaceTracker: function (license, secretKey){
		return 1006;
	},

	QAVSDK_AVContext_InitAgeDectection: function (strBinaryPath, strParamPath){
		return 1006;
	},

	QAVSDK_AVContext_EnableAgeDectection: function (enabled){
		return 1006;
	},

	QAVSDK_AVContext_GetAdvanceParams: function (KeyCode){
		return 1006;
	},

	QAVSDK_AVRoom_GetQualityTips: function (){
		return 1006;
	},

	QAVSDK_AVRoom_ChangeRoomType: function (roomtype, callback){
		return 1006;
	},

	QAVSDK_AVRoom_GetRoomType: function (){
		return 1006;
	},

	QAVSDK_AVRoom_GetRoomID: function (){
		return 1006;
	},

	QAVSDK_AVContext_SetRangeAudioMode: function (audioMode){
		return 1006;
	},

	QAVSDK_AVContext_SetRangeAudioTeamID: function (teamID){
		return 1006;
	},

	QAVSDK_AVContext_SetAudioRole: function (role){
		return 1006;
	},

	QAVSDK_AVRoom_UpdateAudioRecvRange: function (range){
		return 1006;
	},

	QAVSDK_AVRoom_UpdateSelfPosition: function (position, axisForward, axisRight, axisUp, len){
		return 1006;
	},

	QAVSDK_AVRoom_UpdateOtherPosition: function (openID, position, len){
		return 1006;
	},

	QAVSDK_AVRoom_StartRoomSharing: function (targetRoomID, targetOpenID, authBuffer, authBufferLen){
		return 1006;
	},

	QAVSDK_AVRoom_StopRoomSharing: function(){
		return 1006;
	},

	QAVSDK_AVRoom_SwitchRoom: function (roomID, authBuffer, authBufferLen){
		return 1006;
	},

	QAVSDK_AVRoom_SendCustomData: function ( customdata, dataSize,repeatCout){
		return 1006;
	},

	QAVSDK_AVRoom_StopSendCustomData: function (){
		return 1006;
	},

	QAVSDK_AVRoom_SendCustomStreamData: function (customStreamData, length){
		return 1006;
	},

	QAVSDK_AVRoom_SetCustomStreamDataCallback: function (callback, userData){
		return 1006;
	},

	QAVSDK_AVRoom_SetServerAudioRoute: function (SendType, OpenIDforSend, OpenIDforSendSize, RecvType, OpenIDforRecv, OpenIDforRecvSize){
		return 1006;
	},

	QAVSDK_AVRoom_GetCurrentSendAudioRoute: function (OpenIDforSend, OpenIDforSendSize){
		return 1006;
	},

	QAVSDK_AVRoom_GetCurrentRecvAudioRoute: function (OpenIDforRecve, OpenIDforRecveSize){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsAudioCaptureDeviceEnabled: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsAudioPlayDeviceEnabled: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableAudioSend: function (enabled){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableAudioRecv: function (enabled){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsAudioSendEnabled: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsAudioRecvEnabled: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetMicLevel: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetMicVolume: function (volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetMicVolume: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSpeakerLevel: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetSpeakerVolume: function (volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSpeakerVolume: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetSpeakerVolumeByOpenID: function (openId, volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSpeakerVolumeByOpenID: function (openId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetVolumeByUin: function (openId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableLoopBack: function (enabled){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAudioRouteChangeCallback: function (callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_CheckDeviceMuteState: function (callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetVoiceType: function (voiceType){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetKaraokeType: function (type){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_AddAudioBlackList: function (openId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_RemoveAudioBlackList: function (openId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSendStreamLevel: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetRecvStreamLevel: function (openId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_InitSpatializer: function (modelPath){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableSpatializer: function (enabled, applyTeam){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsEnableSpatializer: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAudioMixCount: function (nCount){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_AddSameTeamSpatializer: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_RemoveSameTeamSpatializer: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_AddSpatializerBlacklist: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_RemoveSpatializerBlacklist: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ClearSpatializerBlacklist: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableAudioDispatcher: function (enabled){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StartRecord: function (filePath, sampleRate, channels, recordLocalMic, recordRemote, recordAccompany){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopRecord: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PauseRecord: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumeRecord: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableRecordLocalMic: function (recordLocalMic){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableRecordAccompany: function (recordAccompany){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableRecordRemote: function (recordRemote){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StartRecording: function (type, dstFile, accMixFile, accPlayFile, callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopRecording: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PauseRecording: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumeRecording: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAccompanyFile: function (accPlayFile){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyTotalTimeByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetRecordTimeByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetRecordTimeByMs: function (timePlay, timeRecord){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetRecordKaraokeType: function (type){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetRecordFileDurationByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StartPreview: function (callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopPreview: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PausePreview: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumePreview: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetPreviewTimeByMs: function (time){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetPreviewTimeByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetMixWeights: function (mic, acc){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_AdjustAudioTimeByMs: function (time){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_MixRecordFile: function (needMicData, callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_CancelMixRecordFile: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_CleanTask: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_InitVoiceChanger: function (dataPath){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_FetchVoiceChangerList: function (callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetVoiceChangerName: function (voiceName){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetVoiceChangerParams: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetVoiceChangerParamValue: function (voiceName){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetVoiceChangerParamValue: function (voiceName, voiceValue){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PlayEffect: function (soundId, filePath, loop, pitch, pan, volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PauseEffect: function (soundId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PauseAllEffects: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumeEffect: function (soundId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumeAllEffects: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopEffect: function (soundId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopAllEffects: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetEffectsVolume: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetEffectsVolume: function (volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableEffectSend: function (soundId, enable){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetEffectFileCurrentPlayedTimeByMs: function (soundId, utimeMs){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetEffectVolume: function (soundId){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetEffectVolume: function (soundId, volume){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StartAccompany: function (filePath, loopBack, loopCount, duckerTimeMs, callback){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_StopAccompany: function (duckerTimeMs){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_IsAccompanyPlayEnd: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_PauseAccompany: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_ResumeAccompany: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableAccompanyPlay: function ( enable){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_EnableAccompanyLoopBack: function ( enable){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAccompanyKey: function (nKey){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAccompanyVolume: function (vol){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyVolume: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyFileTotalTimeByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SetAccompanyFileCurrentPlayedTimeByMs: function (utimeMs){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyFileCurrentPlayedTimeByMs: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyFileTotalTimeMsById: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetAccompanyFileCurrentPlayedTimeMsById: function (openid){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetMicListCount: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetMicList: function (devicesInfo, lengthPerDevice, deviceCount){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SelectMic: function (micID){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSpeakerListCount: function (){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_GetSpeakerList: function (devicesInfo, lengthPerDevice, deviceCount){
		return 1006;
	},

	QAVSDK_AVAudioCtrl_SelectSpeaker: function (speaker){
		return 1006;
	},

	QAVSDK_PTT_ApplyPTTAuthbuffer: function (authBuffer, authBufferLen){
		return 1006;
	},

	QAVSDK_PTT_SetAppInfo: function (appid, openid){
		return 1006;
	},

	QAVSDK_PTT_SetMaxMessageLength: function (msTime){
		return 1006;
	},

	QAVSDK_PTT_SetPTTSourceLanguage: function (sourceLanguage){
		return 1006;
	},

	QAVSDK_PTT_StartRecording: function (filePath, callback){
		return 1006;
	},

	QAVSDK_PTT_StopRecording: function (){
		return 1006;
	},

	QAVSDK_PTT_CancelRecording: function (){
		return 1006;
	},

	QAVSDK_PTT_UploadRecordedFile: function (filePath, callback){
		return 1006;
	},

	QAVSDK_PTT_DownloadRecordedFile: function (fileID, filePath, callback){
		return 1006;
	},

	QAVSDK_PTT_StartPlayFileWithVoiceType: function (filePath, voiceType, callback){
		return 1006;
	},

	QAVSDK_PTT_StartPlayFile: function (filePath, callback){
		return 1006;
	},

	QAVSDK_PTT_StopPlayFile: function (){
		return 1006;
	},

	QAVSDK_PTT_GetFileSize: function (filePath){
		return 1006;
	},

	QAVSDK_PTT_GetVoiceFileDuration: function (filePath){
		return 1006;
	},

	QAVSDK_PTT_SpeechToText: function (fileID, speechLanguage, translateLanguage, callback){
		return 1006;
	},

	QAVSDK_PTT_StartRecordingWithStreamingRecognition: function (filePath, speechLanguage, translatelanguage, callback){
		return 1006;
	},

	QAVSDK_PTT_TranslateText: function (text, sourceLanguage, translateLanguage, callback){
		return 1006;
	},

	QAVSDK_PTT_GetMicLevel: function (){
		return 1006;
	},

	QAVSDK_PTT_GetMicVolume: function (){
		return 1006;
	},

	QAVSDK_PTT_SetMicVolume: function (vol){
		return 1006;
	},

	QAVSDK_PTT_GetSpeakerLevel: function (){
		return 1006;
	},

	QAVSDK_PTT_GetSpeakerVolume: function (){
		return 1006;
	},

	QAVSDK_PTT_SetSpeakerVolume: function (vol){
		return 1006;
	},

	QAVSDK_PTT_PauseRecording: function (){
		return 1006;
	},

	QAVSDK_PTT_ResumeRecording: function (){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableMic: function (enable,receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableSpeaker: function (enable,receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableAudioCaptureDevice: function (enable,receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableAudioPlayDevice: function (enable, receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableAudioSend: function (enable, receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_EnableAudioRecv: function (enable, receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_ForbidUserOperation: function (enable, receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_GetMicState: function (receiverID){
		return 1006;
	},

	QAVSDK_AVRoomManager_GetSpeakerState: function (receiverID){
		return 1006;
	},

	QAVSDK_PTT_TextToSpeech: function (text, voiceName, languageCode, speakingRate, callback){
		return 1006;
	},

	QAVSDK_FaceTracker_CreateHandle: function (modelDirPath, configFileName){
		return 1006;
	},

	QAVSDK_FaceTracker_Destroy: function (handle){
		return 1006;
	},

	QAVSDK_FaceTracker_Reset: function (handle){
		return 1006;
	},

	QAVSDK_FaceTracker_GetParam: function (handle, paramPtr){
		return 1006;
	},

	QAVSDK_FaceTracker_SetParam: function (handle, paramPtr){
		return 1006;
	},

	QAVSDK_FaceTracker_TrackFace: function (handle, dataPtr, imageType, width, height, stride, rotate, facesPtr, count){
		return 1006;
	},

	QAVSDK_FaceTracker_ReleaseTrackedFace: function (facesPtr){
		return 1006;
	},

	QAVSDK_FaceRenderer_CreateHandle: function (){
		return 1006;
	},

	QAVSDK_FaceRenderer_LoadAsset: function (assetDirPath, configFileName){
		return 1006;
	},

	QAVSDK_FaceRenderer_Render: function (handle, dstDataPtr, srcDataPtr, imageType, width, height, rotate, facesPtr,  count){
		return 1006;
	},

	QAVSDK_FaceRenderer_Destroy: function (handle){
		return 1006;
	},
		
  //For WebGL, GME only support two callback functions: enterRoomComplete,exitRoomComplete.To keep contact with other platforms,there are some extra callback fuctions which
  //are ineffective. You can just fill in them with NULL.
  QAVSDK_AVContext_SetDelegate: function (enterRoomComplete,exitRoomComplete,roomDisconnect,endpointsUpdateInfo,
                                  onRoomtypeChangeEvent,onDeviceStateChangedEvent,audioReadyCallback,
								  onRoomChangeQualityEvent,commonEventCallback,onEventCallback) {
	    console.log('QAVSDK_AVContext_SetDelegate');
	    function onEvent (eventType, result) {
            if (eventType === gmeAPI.event.ITMG_MAIN_EVENT_TYPE_ENTER_ROOM)
            {
			    gmeAPI.EnableSpeaker(false);
				gmeAPI.EnableMic(false);
			    console.log('Enter Room callback!');
                console.log('onEvent: eventTpye = ' + eventType + 'result = '+ JSON.stringify(result));
                var str2 = JSON.stringify(result);
                var len2 = lengthBytesUTF8(str2) + 1;
                var strPtr2 = _malloc(len2);
                stringToUTF8(str2, strPtr2, len2);
                Module.dynCall_vii(enterRoomComplete, result.errorCode, strPtr2);
            }else if (eventType === gmeAPI.event.ITMG_MAIN_EVENT_TYPE_EXIT_ROOM)
            {
			    console.log('Exit Room callback!');
                console.log('onEvent: eventTpye = ' + eventType + 'result = '+ JSON.stringify(result));
                Module.dynCall_v(exitRoomComplete);
            }else if (eventType === gmeAPI.event.ITMG_MAIN_EVENT_TYPE_USER_UPDATE)
			{
			    //console.log('onEvent: eventTpye = ' + eventType + 'result = '+ JSON.stringify(result));
			}else if (eventType === gmeAPI.event.ITMG_MAIN_EVENT_TYPE_REMOTESTREAM_IN)
			{
			    //console.log('onEvent: eventTpye = ' + eventType + 'result = '+ JSON.stringify(result));
			}else if (eventType === gmeAPI.event.ITMG_MAIN_EVENT_TYPE_REMOTESTREAM_OUT)
			{
			    //console.log('onEvent: eventTpye = ' + eventType + 'result = '+ JSON.stringify(result));
			}
        }
		gmeAPI.SetTMGDelegate(onEvent);
		console.log('QAVSDK_AVContext_SetDelegate = ');
        return 0;
  },
  
   //init for WebGL;sdk_version is not used but it is retained to keep consistent with other platforms
  QAVSDK_AVContext_Start: function (sdk_version,sdkAppId,openId) {
       jsstr_sdkAppId = Pointer_stringify(sdkAppId);
	   jsstr_openId = Pointer_stringify(openId);
	   initFromHtml(document);
	   WebGMEInit(jsstr_sdkAppId,jsstr_openId);
       return 0;   //not implemented
  },
  
  
   QAVSDK_AuthBuffer_GenAuthBuffer: function (appId,roomID,openId,key,authBuffer,authBufferLen) {
	   jsstr_sdkRoomID = Pointer_stringify(roomID);
	   jsstr_openId = Pointer_stringify(openId);
	   jsstr_key = Pointer_stringify(key);
	   JsSharedArray_authBuffer = new Uint8Array(buffer, authBuffer, authBufferLen);
	   length = GenAuthBuffer(appId,jsstr_sdkRoomID,jsstr_openId,jsstr_key,JsSharedArray_authBuffer,authBufferLen);
	   return 0;  
  },
  
  
    QAVSDK_AVContext_EnterRoom: function (roomID,authBuffer,authBufferLen,roomtype) {
       jsstr_sdkRoomID = Pointer_stringify(roomID);
	   EnterRoom(jsstr_sdkRoomID,1);
       return 0;   //not implemented
  },
  
  QAVSDK_AVAudioCtrl_EnableAudioCaptureDevice: function (enabled) {
       if(enabled === 1){
	   gmeAPI.EnableMic(true);
	   console.log('open mic');
	   }else if(enabled ===0){
	   gmeAPI.EnableMic(false);
	   console.log('close mic');
	   }
       return 0;   //not implemented
  },
  
  
  QAVSDK_AVAudioCtrl_EnableAudioPlayDevice: function (enabled) {
       if(enabled === 1){
	   gmeAPI.EnableSpeaker(true);
	   console.log('open mic');
	   }else if(enabled ===0){
	   gmeAPI.EnableSpeaker(false);
	   console.log('close mic');
	   }
       return 0;   //not implemented
  },
  
};

mergeInto(LibraryManager.library,gmesdk);
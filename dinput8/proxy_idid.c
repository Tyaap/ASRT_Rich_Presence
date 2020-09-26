#include "dinputproxy.h"

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceData(struct IDirectInputDevice8 *idid, DWORD cbObjectData, LPDIDEVICEOBJECTDATA rgdod, LPDWORD pdwInOut, DWORD dwFlags)
{
	proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
	return proxydid->did->lpVtbl->GetDeviceData(proxydid->did, cbObjectData, rgdod, pdwInOut, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_QueryInterface(struct IDirectInputDevice8 *idid, REFIID riid, LPVOID * ppvObj)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->QueryInterface(proxydid->did, riid, ppvObj);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_AddRef(struct IDirectInputDevice8 *idid)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    proxydid->refcount++;
    
    return proxydid->did->lpVtbl->AddRef(proxydid->did);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Release(struct IDirectInputDevice8 *idid)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    ULONG ret;
    
    ret = proxydid->did->lpVtbl->Release(proxydid->did);
    
    proxydid->refcount--;
    if (!proxydid->refcount)
    {
        free(proxydid);
    }
    
    return ret;
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetCapabilities(struct IDirectInputDevice8 *idid, LPDIDEVCAPS lpDIDevCaps)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->GetCapabilities(proxydid->did, lpDIDevCaps);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumObjects(struct IDirectInputDevice8 *idid, LPDIENUMDEVICEOBJECTSCALLBACK lpCallback, LPVOID pvRef, DWORD dwFlags)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->EnumObjects(proxydid->did, lpCallback, pvRef, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetProperty(struct IDirectInputDevice8 *idid, REFGUID rguidProp, LPDIPROPHEADER pdiph)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->GetProperty(proxydid->did, rguidProp, pdiph);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetProperty(struct IDirectInputDevice8 *idid, REFGUID rguidProp, LPCDIPROPHEADER pdiph)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->SetProperty(proxydid->did, rguidProp, pdiph);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Acquire(struct IDirectInputDevice8 *idid)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->Acquire(proxydid->did);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Unacquire(struct IDirectInputDevice8 *idid)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->Unacquire(proxydid->did);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceState(struct IDirectInputDevice8 *idid, DWORD cbData, LPVOID lpvData)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->GetDeviceState(proxydid->did, cbData, lpvData);
}


HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetDataFormat(struct IDirectInputDevice8 *idid, LPCDIDATAFORMAT lpdf)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->SetDataFormat(proxydid->did, lpdf);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetEventNotification(struct IDirectInputDevice8 *idid, HANDLE hEvent)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->SetEventNotification(proxydid->did, hEvent);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetCooperativeLevel(struct IDirectInputDevice8 *idid, HWND hwnd, DWORD dwFlags)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->SetCooperativeLevel(proxydid->did, hwnd, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetObjectInfo(struct IDirectInputDevice8 *idid, LPDIDEVICEOBJECTINSTANCE pdidoi, DWORD dwObj, DWORD dwHow)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->GetObjectInfo(proxydid->did, pdidoi, dwObj, dwHow);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceInfo(struct IDirectInputDevice8 *idid, LPDIDEVICEINSTANCE pdidi)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->GetDeviceInfo(proxydid->did, pdidi);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_RunControlPanel(struct IDirectInputDevice8 *idid, HWND hwndOwner, DWORD dwFlags)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->RunControlPanel(proxydid->did, hwndOwner, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Initialize(struct IDirectInputDevice8 *idid, HINSTANCE hinst, DWORD dwVersion, REFGUID rguid)
{
    proxy_IDirectInputDevice *proxydid = (proxy_IDirectInputDevice *)idid;
    
    return proxydid->did->lpVtbl->Initialize(proxydid->did, hinst, dwVersion, rguid);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_CreateEffect(struct IDirectInputDevice8* idid, REFGUID rguid, LPCDIEFFECT lpeff, LPDIRECTINPUTEFFECT* ppdeff, LPUNKNOWN punkOuter)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->CreateEffect(proxydid->did, rguid, lpeff, ppdeff, punkOuter);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumEffects(struct IDirectInputDevice8* idid, LPDIENUMEFFECTSCALLBACKA lpCallback, LPVOID pvRef, DWORD dwEffType)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->EnumEffects(proxydid->did, lpCallback, pvRef, dwEffType);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetEffectInfo(struct IDirectInputDevice8* idid, LPDIEFFECTINFOA pdei, REFGUID rguid)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->GetEffectInfo(proxydid->did, pdei, rguid);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetForceFeedbackState(struct IDirectInputDevice8* idid, LPDWORD pdwOut)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->GetForceFeedbackState(proxydid->did, pdwOut);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SendForceFeedbackCommand(struct IDirectInputDevice8* idid, DWORD dwFlags)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->SendForceFeedbackCommand(proxydid->did, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumCreatedEffectObjects(struct IDirectInputDevice8* idid, LPDIENUMCREATEDEFFECTOBJECTSCALLBACK lpCallback, LPVOID pvRef, DWORD fl)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->EnumCreatedEffectObjects(proxydid->did, lpCallback, pvRef, fl);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Escape(struct IDirectInputDevice8* idid, LPDIEFFESCAPE pesc)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->Escape(proxydid->did, pesc);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Poll(struct IDirectInputDevice8* idid)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->Poll(proxydid->did);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SendDeviceData(struct IDirectInputDevice8* idid, DWORD cbObjectData, LPCDIDEVICEOBJECTDATA rgdod, LPDWORD pdwInOut, DWORD fl)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->SendDeviceData(proxydid->did, cbObjectData, rgdod, pdwInOut, fl);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumEffectsInFile(struct IDirectInputDevice8* idid, LPCSTR lpszFileName, LPDIENUMEFFECTSINFILECALLBACK pec, LPVOID pvRef, DWORD dwFlags)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->EnumEffectsInFile(proxydid->did, lpszFileName, pec, pvRef, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_WriteEffectToFile(struct IDirectInputDevice8* idid, LPCSTR lpszFileName, DWORD dwEntries, LPDIFILEEFFECT rgDiFileEft, DWORD dwFlags)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->WriteEffectToFile(proxydid->did, lpszFileName, dwEntries, rgDiFileEft, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_BuildActionMap(struct IDirectInputDevice8* idid, LPDIACTIONFORMATA lpdiaf, LPCSTR lpszUserName, DWORD dwFlags)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->BuildActionMap(proxydid->did, lpdiaf, lpszUserName, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetActionMap(struct IDirectInputDevice8* idid, LPDIACTIONFORMATA lpdiaf, LPCSTR lpszUserName, DWORD dwFlags)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->SetActionMap(proxydid->did, lpdiaf, lpszUserName, dwFlags);
}

HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetImageInfo(struct IDirectInputDevice8* idid, LPDIDEVICEIMAGEINFOHEADERA lpdiDevImageInfoHeader)
{
    proxy_IDirectInputDevice* proxydid = (proxy_IDirectInputDevice*)idid;

    return proxydid->did->lpVtbl->GetImageInfo(proxydid->did, lpdiDevImageInfoHeader);
}

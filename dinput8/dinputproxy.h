#include <stdio.h>
#include <windows.h>
#include <dinput.h>
#include <time.h>
#pragma pack(1)

// globals
static HINSTANCE hLThis = 0;
static HINSTANCE hL = 0;
static FARPROC p[5] = {0};

// new types
typedef unsigned long uint32;

typedef struct
{
	IDirectInput8Vtbl *lpVtbl;
	IDirectInput8Vtbl vtbl;
	IDirectInput8 *di;
	int refcount;
} proxy_IDirectInput;

typedef struct
{
	IDirectInputDevice8Vtbl *lpVtbl;
	IDirectInputDevice8Vtbl vtbl;
	IDirectInputDevice8 *did;
	int refcount;
	BOOL bKeyboard;
	BOOL bMouse;
	BOOL bControlDown;
	uint32 sequence;
} proxy_IDirectInputDevice;

// IDirectInput vtbl methods
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_QueryInterface(struct IDirectInput *idi, REFIID riid, LPVOID * ppvObj);
ULONG STDMETHODCALLTYPE proxy_IDirectInput_AddRef(struct IDirectInput *idi);
ULONG STDMETHODCALLTYPE proxy_IDirectInput_Release(struct IDirectInput *idi);
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_CreateDevice(struct IDirectInput *idi, REFGUID rguid, LPDIRECTINPUTDEVICEA *lplpDirectInputDevice, LPUNKNOWN pUnkOuter);
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_EnumDevices(struct IDirectInput *idi, DWORD dwDevType, LPDIENUMDEVICESCALLBACKA lpCallback, LPVOID pvRef, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_GetDeviceStatus(struct IDirectInput *idi, REFGUID rguidInstance);
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_RunControlPanel(struct IDirectInput *idi, HWND hwndOnwer, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInput_Initialize(struct IDirectInput *idi, HINSTANCE inst, DWORD dwVersion);

// IDirectInputDevice8 vtbl methods
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_QueryInterface(struct IDirectInputDevice8 *idid, REFIID riid, LPVOID * ppvObj);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_AddRef(struct IDirectInputDevice8 *idid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Release(struct IDirectInputDevice8 *idid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetCapabilities(struct IDirectInputDevice8 *idid, LPDIDEVCAPS lpDIDevCaps);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumObjects(struct IDirectInputDevice8 *idid, LPDIENUMDEVICEOBJECTSCALLBACK lpCallback, LPVOID pvRef, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetProperty(struct IDirectInputDevice8 *idid, REFGUID rguidProp, LPDIPROPHEADER pdiph);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetProperty(struct IDirectInputDevice8 *idid, REFGUID rguidProp, LPCDIPROPHEADER pdiph);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Acquire(struct IDirectInputDevice8 *idid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Unacquire(struct IDirectInputDevice8 *idid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceState(struct IDirectInputDevice8 *idid, DWORD cbData, LPVOID lpvData);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceData(struct IDirectInputDevice8 *idid, DWORD cbObjectData, LPDIDEVICEOBJECTDATA rgdod, LPDWORD pdwInOut, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetDataFormat(struct IDirectInputDevice8 *idid, LPCDIDATAFORMAT lpdf);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetEventNotification(struct IDirectInputDevice8 *idid, HANDLE hEvent);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetCooperativeLevel(struct IDirectInputDevice8 *idid, HWND hwnd, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetObjectInfo(struct IDirectInputDevice8 *idid, LPDIDEVICEOBJECTINSTANCE pdidoi, DWORD dwObj, DWORD dwHow);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetDeviceInfo(struct IDirectInputDevice8 *idid, LPDIDEVICEINSTANCE pdidi);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_RunControlPanel(struct IDirectInputDevice8 *idid, HWND hwndOwner, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Initialize(struct IDirectInputDevice8 *idid, HINSTANCE hinst, DWORD dwVersion, REFGUID rguid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_CreateEffect(struct IDirectInputDevice8* idid, REFGUID rguid, LPCDIEFFECT lpeff, LPDIRECTINPUTEFFECT* ppdeff, LPUNKNOWN punkOuter);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumEffects(struct IDirectInputDevice8* idid, LPDIENUMEFFECTSCALLBACKA lpCallback, LPVOID pvRef, DWORD dwEffType);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetEffectInfo(struct IDirectInputDevice8* idid, LPDIEFFECTINFOA pdei, REFGUID rguid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetForceFeedbackState(struct IDirectInputDevice8* idid, LPDWORD pdwOut);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SendForceFeedbackCommand(struct IDirectInputDevice8* idid, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumCreatedEffectObjects(struct IDirectInputDevice8* idid, LPDIENUMCREATEDEFFECTOBJECTSCALLBACK lpCallback, LPVOID pvRef, DWORD fl);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Escape(struct IDirectInputDevice8* idid, LPDIEFFESCAPE pesc);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_Poll(struct IDirectInputDevice8* idid);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SendDeviceData(struct IDirectInputDevice8* idid, DWORD cbObjectData, LPCDIDEVICEOBJECTDATA rgdod, LPDWORD pdwInOut, DWORD fl);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_EnumEffectsInFile(struct IDirectInputDevice8* idid, LPCSTR lpszFileName, LPDIENUMEFFECTSINFILECALLBACK pec, LPVOID pvRef, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_WriteEffectToFile(struct IDirectInputDevice8* idid, LPCSTR lpszFileName, DWORD dwEntries, LPDIFILEEFFECT rgDiFileEft, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_BuildActionMap(struct IDirectInputDevice8* idid, LPDIACTIONFORMATA lpdiaf, LPCSTR lpszUserName, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_SetActionMap(struct IDirectInputDevice8* idid, LPDIACTIONFORMATA lpdiaf, LPCSTR lpszUserName, DWORD dwFlags);
HRESULT STDMETHODCALLTYPE proxy_IDirectInputDevice_GetImageInfo(struct IDirectInputDevice8* idid, LPDIDEVICEIMAGEINFOHEADERA lpdiDevImageInfoHeader);


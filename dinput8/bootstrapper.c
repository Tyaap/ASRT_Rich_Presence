#include "bootstrapper.h"
#include <Windows.h>
#include <metahost.h>
#pragma comment(lib, "mscoree.lib")

DWORD WINAPI LoadManagedProject(const LPCWSTR managedDllLocation)
{
    HRESULT hr;
    ICLRMetaHost* pClrMetaHost = NULL;
    ICLRRuntimeInfo* pClrRuntimeInfo = NULL;
    ICLRRuntimeHost* pClrRuntimeHost = NULL;

    StartCLR(L"v4.0.30319", &pClrMetaHost, &pClrRuntimeInfo, &pClrRuntimeHost);
    if (pClrRuntimeHost != NULL)
    {
        DWORD result;
        hr = pClrRuntimeHost->lpVtbl->ExecuteInDefaultAppDomain(
            pClrRuntimeHost,
            managedDllLocation,
            L"EntryPoint",
            L"Main",
            L"",
            &result);
        pClrRuntimeHost->lpVtbl->Release(pClrRuntimeHost);
        pClrRuntimeInfo->lpVtbl->Release(pClrRuntimeInfo);
        pClrMetaHost->lpVtbl->Release(pClrMetaHost);
        // We do not clean up the dlls, in case another program is using them.
    }

    return 0;
}

void StartCLR(
    LPCWSTR dotNetVersion,
    ICLRMetaHost** ppClrMetaHost,
    ICLRRuntimeInfo** ppClrRuntimeInfo,
    ICLRRuntimeHost** ppClrRuntimeHost)
{
    HRESULT hr;

    // Get the CLRMetaHost that tells us about .NET on this machine
    hr = CLRCreateInstance(&CLSID_CLRMetaHost, &IID_ICLRMetaHost, (LPVOID*)ppClrMetaHost);
    if (hr == S_OK)
    {
        // Get the runtime information for the particular version of .NET
        hr = (*ppClrMetaHost)->lpVtbl->GetRuntime(*ppClrMetaHost, dotNetVersion, &IID_ICLRRuntimeInfo, (LPVOID*)ppClrRuntimeInfo);
        if (hr == S_OK)
        {
            // Check if the specified runtime can be loaded into the process. This
            // method will take into account other runtimes that may already be
            // loaded into the process and set pbLoadable to TRUE if this runtime can
            // be loaded in an in-process side-by-side fashion.
            BOOL fLoadable;
            hr = (*ppClrRuntimeInfo)->lpVtbl->IsLoadable(*ppClrRuntimeInfo, &fLoadable);
            if ((hr == S_OK) && fLoadable)
            {
                // Load the CLR into the current process and return a runtime interface
                // pointer.
                hr = (*ppClrRuntimeInfo)->lpVtbl->GetInterface(
                    *ppClrRuntimeInfo,
                    &CLSID_CLRRuntimeHost,
                    &IID_ICLRRuntimeHost,
                    (LPVOID*)ppClrRuntimeHost);
                if (hr == S_OK)
                {
                    // Start it. This is okay to call even if the CLR is already running
                    (*ppClrRuntimeHost)->lpVtbl->Start(*ppClrRuntimeHost);
                    return;
                }
            }
        }
    }
    // Cleanup if failed
    if (*ppClrRuntimeHost)
    {
        (*ppClrRuntimeHost)->lpVtbl->Release(*ppClrRuntimeHost);
        (*ppClrRuntimeHost) = NULL;
    }
    if (*ppClrRuntimeInfo)
    {
        (*ppClrRuntimeInfo)->lpVtbl->Release(*ppClrRuntimeInfo);
        (*ppClrRuntimeInfo) = NULL;
    }
    if (*ppClrMetaHost)
    {
        (*ppClrMetaHost)->lpVtbl->Release(*ppClrMetaHost);
        (*ppClrMetaHost) = NULL;
    }
    return;
}
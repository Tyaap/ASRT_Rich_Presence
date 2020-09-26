#pragma once
#include <metahost.h>

DWORD WINAPI LoadManagedProject(const PWSTR managedDllLocation);

void StartCLR(
    LPCWSTR dotNetVersion,
    ICLRMetaHost** ppClrMetaHost,
    ICLRRuntimeInfo** ppClrRuntimeInfo,
    ICLRRuntimeHost** ppClrRuntimeHost);
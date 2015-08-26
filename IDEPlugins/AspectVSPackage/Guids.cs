// Guids.cs
// MUST match guids.h
using System;

namespace MMX.AspectVSPackage
{
    static class GuidList
    {
        public const string guidAspectPackagePkgString = "547147e6-b480-4671-9e06-242a945b9957";
        public const string guidAspectPackageCmdSetString = "8ee560b0-83c7-43ac-8faa-7825b8b32c4f";
        public const string guidAspectWindow = "DFEA36B7-C999-403D-93D1-C1D5A7E5669F";

        public static readonly Guid guidAspectVSPackage2CmdSet = new Guid(guidAspectPackageCmdSetString);
    };
}
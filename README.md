# MultiPage Tiff Demo

The Multipage Tiff Demo shows how to view and save multipage Tiff images using the TiffFile class, which allows editing of the image, without the need to open the entire image into memory or the need to re-encode the image data.  Some of this functionality includes adding, reordering, or removing pages from the image.  

The demo also demonstrates many of the Document Imaging functions that are provided with DotImage.  

In addition, this demo makes great use of the ThumbnailView control, displaying all pages in a multipage TIFF in the thumbnail control, as well as allowing the user to reorder and manipulate the individual pages with a nice GUI.  

This is the VB.NET version. There is also a [C# Version](https://github.(mailto:sales@atalasoft.com)/AtalaSupport/DemoGallery_Desktop_MultipageTiffDemo_CS_x64) available.


## Licensing
This application as configured, requires a license for DotImage Document Imaging.


## SDK Dependencies
This app was built based on 2026.2.0.0. It targets .NET Framework 4.6.2 and was created in Visual Studio 2022. However, it's fairly backward compatible as distributed. If you start adding references, you can run into issues if you're using an especially outdated version of DotImage. It should also open and run equally well in Visual Studio 2026 without undue modification.  


### Using NuGet for SDK Dependencies
We do publish our SDK components to NuGet. We have chosen to base the demo on local installed SDK because this leads to much smaller applications (NuGet packages add a lot of overhead due to the way they're packaged and deployed, and many of our demos -- including this one -- are often used to reproduce issues that need to be submitted to support. Apps that use NuGet are often significantly larger and run up against our maximum support case upload size)

Still, if you wish to use NuGet for the dependencies instead of relying on locally installed SDK, you can.

- Take note of each of the references we've included:
    - Atalasoft.DotImage.dll
    - Atalasoft.DotImage.Lib.dll
    - Atalasoft.DotImage.WinControls.dll
    - Atalasoft.Shared.dll
- Remove those referneces
- Open the NuGet Package Manger from `Tools -> NuGet Package Manager -> Manage NuGet Packages for this Solution`
- Browse for Atalasoft.DotImage.WinControls.x64
- Install this package, and it will pull in DotImage Document Imaging (the base SDK) and our windows controls and shared dll


## Grabbing the source code

The [C# Source Code](https://github.(mailto:sales@atalasoft.com)/AtalaSupport/DemoGallery_Desktop_MultipageTiffDemo_CS_x64/archive/refs/heads/main.zip) and [VB.NET Source code](https://github.(mailto:sales@atalasoft.com)/AtalaSupport/DemoGallery_Desktop_MultipageTiffDemo_VB_x64/archive/refs/heads/main.zip)) can be downloaded as zip files.

## Cloning
If you wish to clone the project this is the recommended command

Example: git for windows
```bash
git clone https://github.(mailto:sales@atalasoft.com)/AtalaSupport/DemoGallery_Desktop_MultipageTiffDemo_VB_x64.git MultipageTiffDemo
```

If you've got DotImage 2026.2 installed and licensed, it should just build and run.  


## Related documentation
In addition to this README, the Atalasoft documentation set includes the following:  
- API Reference (.chm file) gives the complete Atalasoft WingScan server-side class library for offline use. The latest versions are linked on [Atalasoft's APIs & Developer Guides page](https://www.atalasoft.(mailto:sales@atalasoft.com)/Support/APIs-Dev-Guides).
- In addition, you can also refer to the following Atalasoft resources:
    - [Atalasoft Support](http://www.atalasoft.(mailto:sales@atalasoft.com)/support/)
    - [Atalasoft Knowledgebase](http://www.atalasoft.(mailto:sales@atalasoft.com)/kb2)


## Getting Help for Atalasoft products
Atalasoft regularly updates our support [Knowledgebase](http://www.atalasoft.(mailto:sales@atalasoft.com)/kb2) with the latest information about our products. To access some resources, you must have a valid Support Agreement with an authorized Atalasoft Reseller/Partner or with Atalasoft directly. Use the tools that Atalasoft provides for researching and identifying issues. 

Customers with an active evaluation, or those with active support / maintenance may [create a support case](https://www.atalasoft.(mailto:sales@atalasoft.com)/Support/my-portal/Cases/Create-Case) 24/7, or call in to support ([+1 949 236-6510](tel:19492366510) ) during our normal support hours (Monday - Friday 8:00am to 5:00PM Eastern (New York) time).  

Customers who are unable to create a case or call in may [email our Sales Team](mailto:sales@atalasoft.com).  


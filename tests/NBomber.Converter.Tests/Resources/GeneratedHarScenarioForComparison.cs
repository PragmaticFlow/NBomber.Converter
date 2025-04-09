using NBomber.CSharp;
using NBomber.Http.CSharp;
using System.Text;
using System.Net.Http;

public class HelloWorldExample
{
    public void Run()
    {
        using var httpClient = new HttpClient();

        var scn1 = Scenario.Create("scenario_1", async context =>
        {
            var step1 = await Step.Run("step 1 - GET reqres.in/api/users?page=2", context, async () =>
            {
                var request = Http.CreateRequest("GET", "https://reqres.in/api/users?page=2")
                    .WithHeader("accept", "*/*")
                    .WithHeader("accept-encoding", "gzip, deflate, br, zstd")
                    .WithHeader("accept-language", "en-US,en;q=0.9")
                    .WithHeader("cache-control", "no-cache")
                    .WithHeader("content-type", "application/json")
                    .WithHeader("pragma", "no-cache")
                    .WithHeader("priority", "u=1, i")
                    .WithHeader("referer", "https://reqres.in/")
                    .WithHeader("sec-ch-ua", "\"Opera\";v=\"116\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"")
                    .WithHeader("sec-ch-ua-mobile", "?0")
                    .WithHeader("sec-ch-ua-platform", "\"Windows\"")
                    .WithHeader("sec-fetch-dest", "empty")
                    .WithHeader("sec-fetch-mode", "cors")
                    .WithHeader("sec-fetch-site", "same-origin")
                    .WithHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 OPR/116.0.0.0");

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step2 = await Step.Run("step 2 - POST reqres.in/api/users", context, async () =>
            {
                var request = Http.CreateRequest("POST", "https://reqres.in/api/users")
                    .WithHeader("accept", "*/*")
                    .WithHeader("accept-encoding", "gzip, deflate, br, zstd")
                    .WithHeader("accept-language", "en-US,en;q=0.9")
                    .WithHeader("cache-control", "no-cache")
                    .WithHeader("content-type", "application/json")
                    .WithHeader("origin", "https://reqres.in")
                    .WithHeader("pragma", "no-cache")
                    .WithHeader("priority", "u=1, i")
                    .WithHeader("referer", "https://reqres.in/")
                    .WithHeader("sec-ch-ua", "\"Opera\";v=\"116\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"")
                    .WithHeader("sec-ch-ua-mobile", "?0")
                    .WithHeader("sec-ch-ua-platform", "\"Windows\"")
                    .WithHeader("sec-fetch-dest", "empty")
                    .WithHeader("sec-fetch-mode", "cors")
                    .WithHeader("sec-fetch-site", "same-origin")
                    .WithHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 OPR/116.0.0.0");

                request.WithBody(new StringContent("""{"name":"morpheus","job":"leader"}""", Encoding.UTF8, "application/json"));

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step3 = await Step.Run("step 3 - PUT clients6.google.com/upload/drive/v2internal/batch?key=AIzaSyD_InbmSFufIEps5UAt2NmB_3LvBH3Sz_8", context, async () =>
            {
                var request = Http.CreateRequest("PUT", "https://clients6.google.com/upload/drive/v2internal/batch?key=AIzaSyD_InbmSFufIEps5UAt2NmB_3LvBH3Sz_8")
                    .WithHeader("accept", "*/*")
                    .WithHeader("accept-encoding", "gzip, deflate, br, zstd")
                    .WithHeader("accept-language", "en-US,en;q=0.9")
                    .WithHeader("cache-control", "no-cache")
                    .WithHeader("content-type", "multipart/mixed; boundary=\"=====gdmqe2qcq4t3=====\"")
                    .WithHeader("origin", "https://drive.google.com")
                    .WithHeader("pragma", "no-cache")
                    .WithHeader("priority", "u=1, i")
                    .WithHeader("referer", "https://drive.google.com/")
                    .WithHeader("sec-ch-ua", "\"Opera\";v=\"116\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"")
                    .WithHeader("sec-ch-ua-mobile", "?0")
                    .WithHeader("sec-ch-ua-platform", "\"Windows\"")
                    .WithHeader("sec-fetch-dest", "empty")
                    .WithHeader("sec-fetch-mode", "cors")
                    .WithHeader("sec-fetch-site", "same-site")
                    .WithHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 OPR/116.0.0.0")
                    .WithHeader("x-client-data", "CJ/5ygE=")
                    .WithHeader("x-goog-authuser", "0")
                    .WithHeader("x-goog-upload-protocol", "batch");

                request.WithBody(new StringContent("""--=====gdmqe2qcq4t3=====content-type: application/httpPOST /upload/drive/v2internal/files?uploadType=multipart&supportsTeamDrives=true&fields=kind%2Cparents(id)%2CmodifiedDate%2ChasVisitorPermissions%2CcontainsUnsubscribedChildren%2Ccapabilities(canMoveItemIntoTeamDrive%2CcanUntrash%2CcanModifyContentRestriction%2CcanMoveItemWithinTeamDrive%2CcanMoveItemOutOfTeamDrive%2CcanDeleteChildren%2CcanTrashChildren%2CcanRequestApproval%2CcanReadCategoryMetadata%2CcanEditCategoryMetadata%2CcanAddMyDriveParent%2CcanRemoveMyDriveParent%2CcanShareChildFiles%2CcanShareChildFolders%2CcanRead%2CcanMoveItemWithinDrive%2CcanMoveChildrenWithinDrive%2CcanAddFolderFromAnotherDrive%2CcanChangeSecurityUpdateEnabled%2CcanBlockOwner%2CcanReportSpamOrAbuse%2CcanCopyNonAuthoritative%2CcanDownloadNonAuthoritative%2CcanReportNotSpam%2CcanInitiateEsignature%2CcanCopy%2CcanDownload%2CcanEdit%2CcanAddChildren%2CcanDelete%2CcanRemoveChildren%2CcanShare%2CcanTrash%2CcanRename%2CcanListChildren%2CcanReadTeamDrive%2CcanMoveTeamDriveItem)%2CmodifiedByMeDate%2ClastViewedByMeDate%2CalternateLink%2CworkspaceIds%2CfileSize%2CcontentRestrictions(readOnly)%2CapprovalMetadata(approvalVersion%2CapprovalSummaries%2ChasIncomingApproval)%2Cowners(kind%2CpermissionId%2CdisplayName%2Cpicture%2CemailAddress%2Cid)%2CshortcutDetails(targetId%2CtargetMimeType%2CtargetLookupStatus%2CtargetFile%2CcanRequestAccessToTarget)%2ClastModifyingUser(kind%2CpermissionId%2CdisplayName%2Cpicture%2CemailAddress%2Cid)%2CcustomerId%2CancestorHasAugmentedPermissions%2ChasThumbnail%2CthumbnailVersion%2CclientEncryptionDetails(encryptionState)%2Ctitle%2Cid%2CresourceKey%2CabuseIsAppealable%2CabuseNoticeReason%2CspamMetadata(markedAsSpamDate%2CinSpamView%2CisSpam%2CisInheritedSpam)%2Cshared%2CaccessRequestsCount%2CsharedWithMeDate%2CuserPermission(role)%2CinheritedPermissionsDisabled%2CexplicitlyTrashed%2CmimeType%2CquotaBytesUsed%2Csubscribed%2CfolderColor%2ChasChildFolders%2Clabels(starred%2Ctrashed%2Crestricted%2Cviewed)%2CfileExtension%2CprimarySyncParentId%2CsharingUser(kind%2CpermissionId%2CdisplayName%2Cpicture%2CemailAddress%2Cid)%2CflaggedForAbuse%2CfolderFeatures%2Cspaces%2CsourceAppId%2Crecency%2CrecencyReason%2Cversion%2CheadRevisionId%2CactionItems%2CteamDriveId%2ChasAugmentedPermissions%2CcreatedDate%2CprimaryDomainName%2CorganizationDisplayName%2CpassivelySubscribed%2CtrashingUser(kind%2CpermissionId%2CdisplayName%2Cpicture%2CemailAddress%2Cid)%2CtrashedDate&pinned=true&convert=false&openDrive=false&reason=202&syncType=0&errorRecovery=false&key=AIzaSyD_InbmSFufIEps5UAt2NmB_3LvBH3Sz_8 HTTP/1.1content-type: multipart/related; boundary="m524lqhemqpb"authorization: SAPISIDHASH 1739812275_105e518d11ed9e97023efb032ee43343f2178324_u SAPISID1PHASH 1739812275_105e518d11ed9e97023efb032ee43343f2178324_u SAPISID3PHASH 1739812275_105e518d11ed9e97023efb032ee43343f2178324_ux-goog-authuser: 0host: clients6.google.comorigin: https://drive.google.com--m524lqhemqpbcontent-type: application/json; charset=UTF-8{"title":"1.jpg","mimeType":"image/jpeg","parents":[{"id":"0AGbMrZxpbUPYUk9PVA"}],"modifiedDate":"2025-02-17T16:48:52Z"}--m524lqhemqpbcontent-transfer-encoding: base64content-length: 984content-type: image/jpeg/9j/4AAQSkZJRgABAQEAeAB4AAD/4QAiRXhpZgAATU0AKgAAAAgAAQESAAMAAAABAAEAAAAAAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCAcHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAeAB4DASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9+mXAox0obJasHxD42j8O+KtB0lrDVrl9elmiS5trN5re0McTS5nkXiFWClVLYDOQo5IBNEVGEpO0f6tqdBRRRQSFBGTRRQAUUUUAf//Z--m524lqhemqpb----=====gdmqe2qcq4t3=====--""", Encoding.UTF8, "multipart/mixed"));

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step4 = await Step.Run("step 4 - DELETE reqres.in/api/users/2", context, async () =>
            {
                var request = Http.CreateRequest("DELETE", "https://reqres.in/api/users/2")
                    .WithHeader("accept", "*/*")
                    .WithHeader("accept-encoding", "gzip, deflate, br, zstd")
                    .WithHeader("accept-language", "en-US,en;q=0.9")
                    .WithHeader("cache-control", "no-cache")
                    .WithHeader("content-type", "application/json")
                    .WithHeader("origin", "https://reqres.in")
                    .WithHeader("pragma", "no-cache")
                    .WithHeader("priority", "u=1, i")
                    .WithHeader("referer", "https://reqres.in/")
                    .WithHeader("sec-ch-ua", "\"Opera\";v=\"116\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"")
                    .WithHeader("sec-ch-ua-mobile", "?0")
                    .WithHeader("sec-ch-ua-platform", "\"Windows\"")
                    .WithHeader("sec-fetch-dest", "empty")
                    .WithHeader("sec-fetch-mode", "cors")
                    .WithHeader("sec-fetch-site", "same-origin")
                    .WithHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 OPR/116.0.0.0");

                var response = await Http.Send(httpClient, request);

                return response;
            });

            return Response.Ok();
        })
        .WithRestartIterationOnFail(shouldRestart: false);

        NBomberRunner
            .RegisterScenarios(scn1)
            .Run();
    }
}
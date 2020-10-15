(*
12-Oct-2020
Rewriting of the app https://github.com/recs12/save_pdf in F#.
*)

open System
open SolidEdgeCommunity.Extensions // https://github.com/SolidEdgeCommunity/SolidEdge.Community/wiki/Using-Extension-Methods
open SolidEdgeFramework
open SolidEdgeCommunity
open SolidEdgeDraft

// Open connection to solidedge.

let app = SolidEdgeCommunity.SolidEdgeUtils.Connect(true, true)

// Connect to the  active doc.
let document = app.ActiveDocument

printfn "Active document type: %A" app.ActiveDocumentType

// Get the type of document: is it an assembly, part, sheet, draft
let (|Draft|Part|Sheet|Assembly|Unknown|) obj =
    if app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igPartDocument then Part
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igDraftDocument then Draft
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igAssemblyDocument then Assembly
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument then Sheet
    else Unknown

let pathToMyDesktop = @"C:\Users\Slimane\Desktop\print\draft.pdf"

let makePdf draft = draft.SaveAs(NewName=pathToMyDesktop, FileFormat=False)

let PrintAsPDF obj document =
    match obj with
    // | Draft -> makePdf document
    | Draft -> printfn "Save as PDF."
    | _ -> printfn "This isn't a draft document, it can't be printed."


PrintAsPDF app |> ignore

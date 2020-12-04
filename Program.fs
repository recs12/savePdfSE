(*
12-Oct-2020
Rewriting of the app https://github.com/recs12/save_pdf in F#.
*)

open System
open SolidEdgeCommunity
open SolidEdgeCommunity.Extensions // https://github.com/SolidEdgeCommunity/SolidEdge.Community/wiki/Using-Extension-Methods
open SolidEdgeFramework
open SolidEdgeDraft
open SolidEdgePart


[<STAThread>]
[<EntryPoint>]
SolidEdgeCommunity.OleMessageFilter.Register ();

let pathToMyDesktop:string = @"C:\Users\Slimane\Desktop\print\Fpart2.pdf"


// Open connection to solidedge.
let app:SolidEdgeFramework.Application = SolidEdgeCommunity.SolidEdgeUtils.Connect(true, true)

printfn "Active document type: %A" app.ActiveDocumentType


// Get the type of document: is it an assembly, part, sheet, draft
let (|Draft|Part|Sheet|Assembly|Unknown|) obj =
    if app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igPartDocument then Part
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igDraftDocument then Draft
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igAssemblyDocument then Assembly
    elif app.ActiveDocumentType = SolidEdgeFramework.DocumentTypeConstants.igSheetMetalDocument then Sheet
    else Unknown

type DraftDoc =
     interface SolidEdgeFramework.SolidEdgeDocument with 
         member this.SaveAs(a)

let m = new DraftDoc()
let M = m :> DraftDoc

let makePdf (drawing : SolidEdgeDraft.DraftDocument) pathToMyDesktop =
    drawing.SaveAs(pathToMyDesktop)

let PrintAsPDF obj =
    match obj with
    | Draft ->
        makePdf M pathToMyDesktop
    // | Draft -> printfn "Save as PDF."
    | _ -> printfn "This isn't a draft document, it can't be printed."


PrintAsPDF app|> ignore

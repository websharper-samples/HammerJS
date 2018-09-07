namespace Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Html
open WebSharper.UI.Html.Elt
open WebSharper.UI.Client
open WebSharper.HammerJS


[<JavaScript>]
module Client =    
   
    [<SPAEntryPoint>]
    let Main () =

        // Simple Hammer

        let d1 =
            div [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        let hammer1 = Hammer(d1.Dom)

        hammer1.On("panleft panright tap press", fun ev ->
                DocExtensions.Clear(d1)
                let b = div [] [text (ev.Type + "gesture detected")]
                d1.Dom.AppendChild(b.Dom) |> ignore
            )

        let d2 =
            div [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        // Swipe

        let hammer2 = Hammer.Manager(d2.Dom)


        let cfg = SwipeConf(Direction = Hammer.DIRECTION_ALL)
        
        hammer2.Add(Hammer.Swipe(cfg))

        hammer2.On("swipeleft swiperight swipeup swipedown panleft panright tap press", fun ev ->
                DocExtensions.Clear(d2)
                let b = div [] [text (ev.Type + "gesture detected")]
                d2.Dom.AppendChild(b.Dom) |> ignore
            )

        let d3 =
            div [
                    attr.style "background : silver; height : 150px; text-align: center; font: 15px/150px Helvetica, Arial, sans-serif;"
                ]
                []

        // Single vs Double

        let hammer3 = Hammer.Manager(d3.Dom)

        hammer3.Add(Hammer.Tap(TapConf(Event = "doubletap", Taps = 2)))
        hammer3.Add(Hammer.Tap(TapConf(Event = "singletap")))
        hammer3.Get("doubletap").RecognizeWith("singletap")
        hammer3.Get("singletap").RequireFailure("doubletap")

        hammer3.Add(Hammer.Pan(PanConf(Direction=Hammer.DIRECTION_ALL)))

        hammer3.On("singletap doubletap panleft panright press", fun ev ->
                DocExtensions.Clear(d3)
                let b = div [] [text (ev.Type + "gesture detected")]
                d3.Dom.AppendChild(b.Dom) |> ignore
            )

        let cont =
            div []
                [
                    h2 [] [text "Simple hammer class"]
                    d1
                    h2 [] [text "This one only detects swipe"]
                    d2
                    h2 [] [text "Doubletap will not trigger singletap"] 
                    d3
                ]

        cont |> Doc.RunById "main"
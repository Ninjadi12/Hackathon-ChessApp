#pragma checksum "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "df32c9958bdfd204a13f4f9bb52f55a1c1984fd3"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Projects.Pages
{
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Projects;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\_Imports.razor"
using Projects.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor"
using Toolbelt.Blazor.SpeechRecognition;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor"
using System.Text.RegularExpressions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor"
using Toolbelt.Blazor.SpeechSynthesis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor"
using System;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Game")]
    public partial class Game : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 140 "C:\Users\gwyd0\OneDrive\Documents\Hackathon\Pages\Game.razor"
       
    
    public string[] pog = Enumerable.Repeat("", 64).ToArray(); 
    SpeechRecognitionResult[] Results = new SpeechRecognitionResult[0];
    Chess game = new Chess();
    public string PlayerTurn;
    string turn = "white";
    Regex rx = new Regex(@"^\s*[a-zA-Z]{1}\d{1}\s*$");
    Square[,] board;
    int counter = 0;
    int checking = 0;
    string origin = "";
    string destination = "";


    private void doStuff()
    {
      board = game.getBoard();
      counter = 0;
      for (int i = 0;i < 8; i++)
      {
       for (int j = 0; j < 8; j++)
       {
         if(board[i, j].hasPiece())
         {
           
           pog[counter] = board[i,j].getPiece().getSymbol();
         }
         else
         {
           pog[counter] = " ";
         }
         counter++;
       }
      }
      PlayerTurn = "It's " + turn + "'s turn!";
    }

    

    protected void play()
    {
        game.main();
        doStuff();
    }
    protected override void OnInitialized()
    {
       speech_recognition.InterimResults = true;
       speech_recognition.Continuous = true;
       speech_recognition.Result += OnSpeechRecognized;
    }

    void OnSpeechRecognized(object sender, SpeechRecognitionEventArgs args)
    {
      Results = args.Results.Skip(args.ResultIndex).ToArray();
      foreach (var result in Results)
       {
          if (result.IsFinal)
          {
              if (rx.IsMatch(result.Items[0].Transcript))
              {
                if (origin == "") {
                  origin = result.Items[0].Transcript; 
                  speech_synthesis.Speak("Tell me the destination");
                }
                else {
                  destination = result.Items[0].Transcript;
                  speech_synthesis.Speak("Would you like to play " + origin + " to " + destination);
                }
              }
              else if ((result.Items[0].Transcript.Trim(' ') == "yes" || result.Items[0].Transcript.Trim(' ') == "yeah"))
              {
                Console.WriteLine("playing");
                speech_synthesis.Speak("Okay");
                checking = game.play(origin, destination);
                Console.WriteLine(checking.ToString());
                if(checking == 0)
                {
                  speech_synthesis.Speak("Valid move");
                  doStuff();
                  if (turn == "white")
                    turn = "black";
                  else 
                    turn = "white";
                origin = "";
                destination = "";
                }
                else if(checking == 1)
                {
                  speech_synthesis.Speak("Invalid move");
                origin = "";
                destination = "";
                }
                else if(checking == 2)
                {
                  speech_synthesis.Speak("Invalid, you will be in check");
                  doStuff();
                origin = "";
                destination = "";
                }
                else if(checking == 3)
                {
                  speech_synthesis.Speak("Valid move, opponent is in check");
                  doStuff();
                  if (turn == "white")
                    turn = "black";
                  else 
                    turn = "white";
                }
                Console.WriteLine("Printed");
                //speech_recognition.Result -= OnSpeechRecognized;
                origin = "";
                destination = "";
              } 
              else if(origin != "")
              {
                speech_synthesis.Speak("Tell me the destination");
              }
              else if (origin == "" && destination == "")
              {
                speech_synthesis.Speak("Tell me the origin for player " + turn);
              }      
         }
         
       }
      StateHasChanged();
    }
    async Task OnClickStart()
    {
      await speech_recognition.StartAsync();
    }
    async Task OnClickStop()
    {
        await speech_recognition.StopAsync();
    }
    
    public void Dispose()
    {
      speech_recognition.Result -= OnSpeechRecognized;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SpeechSynthesis speech_synthesis { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private SpeechRecognition speech_recognition { get; set; }
    }
}
#pragma warning restore 1591

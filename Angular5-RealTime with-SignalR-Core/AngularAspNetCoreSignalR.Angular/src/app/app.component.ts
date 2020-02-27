import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private _hubConnection: HubConnection;
  private _hubConnection2: HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];

  public sendMessage(): void {
    this._hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .then(() => this.message = '')
      .catch(err => console.error(err));
  }

  ngOnInit() {
    this.nick ='John';// window.prompt('Your name:', 'John');

    this._hubConnection = new HubConnection('http://localhost:5000/chat');

    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

      this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
        const text = `${nick}: ${receivedMessage}`;
        this.messages.push(text);
      }); 


      this._hubConnection2 = new HubConnection("http://localhost:33383/gauges");
    
    this._hubConnection2
      .start()
      .then(() => this._hubConnection2.invoke('GetGaugesDataList').catch(err => console.error(err)))
      .catch(err => console.log('Error while establishing connection :('));

    var that = this;
    this._hubConnection2.on('GetGaugesDataList', (data: GaugeModel) => {
   debugger;

      this.data =  data
    });
    
    }

   data :GaugeModel;
}
export class GaugeModel {
  public id: number;
  public cpu: number;
  public memory: number;
  public network: number;
}


// CREATE TABLE [dbo].[Notification](  
//   [Id] [int] IDENTITY(1,1) NOT NULL,  
//   [Type] [int] NULL,  
//   [Details] [nvarchar](500) NULL,  
//   [Title] [nvarchar](50) NULL,  
//   [DetailsURL] [nvarchar](500) NULL,  
//   [SentTo] [nvarchar](50) NULL,  
//   [Date] [date] NULL,  
//   [IsRead] [bit] NULL,  
//   [IsDeleted] [bit] NULL,  
//   [IsReminder] [bit] NULL,  
//   [Code] [nvarchar](100) NULL,  
//   [NotificationType] [nvarchar](100) NULL,  
// CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED   
// (  
//   [Id] ASC  
// )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
// ) ON [PRIMARY]  
// GO


// SET ANSI_NULLS ON
// GO

// SET QUOTED_IDENTIFIER ON
// GO

// CREATE TABLE [dbo].[Messages](
// 	[MessageID] [int] IDENTITY(1,1) NOT NULL,
// 	[Message] [nvarchar](50) NULL,
// 	[EmptyMessage] [nvarchar](50) NULL,
// 	[Date] [datetime] NULL,
//  CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
// (
// 	[MessageID] ASC
// )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
// ) ON [PRIMARY]

// GO

// ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_Date]  DEFAULT (getdate()) FOR [Date]
// GO

// ALTER DATABASE [ZRamzi_DB] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE ;

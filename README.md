# Overview
The Chain of Responsibility pattern is a behavioral design pattern used in software engineering to achieve loose coupling in request handling. It allows an object to pass a request along a chain of potential handlers until one of them handles the request. Each handler in the chain either handles the request or forwards it to the next handler. This approach allows multiple handlers to process the request without the sender needing to know which handler will ultimately process it.

Handler: Defines an interface for handling requests and optionally holds a reference to the next handler in the chain.
ConcreteHandler: Implements the Handler interface and processes the request. If it can’t handle the request, it forwards it to the next handler in the chain.
Client: Initiates the request and starts the chain.

# Project Description
This project defines approval of different types of requests. Idea of this project came from the IAR cell which I'm part of. 

There are two types of users - 

1. Requesters  2. Approvers
   
Requester contains -  lead1,lead2,lead3

Approver contains - student head, executive,faculty incharge

Each user has a position and password corresponding to them.

When user logs in according to their position certain choices will be displayed

If user position is in requester then they have 2 choices

	1. Send requests for approvers
 
	2. View their all of their sent requests from the message
 
If user position is in approvers then they have 2 choices

    1.View received requests and approve or reject them 

    2. View their all received requests from the message log
![Class diagram]Screenshot_20240903_224549_Adobe Scan.jpg)
There are 3 types of requests 

EventIdeaRequest -  which is sent to only student head of approvers

Budget request - which is sent according to different condition

	1. If budget <2000 send it to student head and executive.
   
	2. If budget >2000 send it to student head, executive and faculty incharge
   
Amendment requests - which is sent to student head, executive and faculty incharge

All requests have description and only budget request contains description+budget

Message log - each user will have their own message log and it contains message,status and remarks.Only corresponding user can see their respective message log.

We use chain of responsibility in handling the requests to different approvers.

We are also saving the requests in a json file to retreive the data when needed.

Here UserInterface is Client , RequestHandlers are Handler for different requests.


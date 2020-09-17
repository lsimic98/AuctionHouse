//SignalR sheet

var connection = new signalR.HubConnectionBuilder ( ).withUrl ( "/update" ).build ( );


function handleError(error)
{
    alert(error);
}

connection.start().then(
    function()
    {
       // alert("Connecton started!");
    }
    ).catch(handleError);












//Bid and timer
function jumpToPage(pageNumber)
{

    var pageNum = parseInt(pageNumber);
    pageNum = pageNum<=0 ? 1 : pageNum;

    var search = $("#search").val();
    if(search == "")
        search = null;
    
    var minPrice = $("#minPrice").val();
    if(minPrice == "")
        minPrice = null;
    else
        minPrice = parseInt(minPrice);
    if(minPrice<0)
    {
        minPrice = 0;
        $("#minPrice").val(minPrice);
    }

    var maxPrice = $("#maxPrice").val();
    if(maxPrice == "")
        maxPrice = null;
    else
        maxPrice = parseInt(maxPrice);
    if(maxPrice<0)
    {
        maxPrice = 1;
        $("#maxPrice").val(maxPrice);
    }

    if(maxPrice!=null && minPrice!=null && minPrice>maxPrice)
    {
        alert("Max price must be greater than min price, or leave max price empty!");
    }
    
    var state = $("#state").children("option:selected").val();
    if(state=="null")
        state=null;

        $.ajax ({  
            type: "POST", 
            url: "/Auction/JumpToPage", 
            data: { 
                 "pageNumber":pageNum,
                 "search":search,
                 "minPrice":minPrice,
                 "maxPrice":maxPrice,
                 "state":state
            },
            dataType: "text",
            success: function ( response ) {
                $("#auctions").html(response);
                //alert("Page successfully refreshed :D");
            },
            error: function ( response ) {
                alert ( response ) 
            }
        });
}

function bid(aId, bOffer)
{
    var auctionId = parseInt(aId);
    var bidOffer = parseInt(bOffer);
    var verificationToken = $("input[name='__RequestVerificationToken']").val( );


    if(auctionId<=0 || bidOffer<=0)
    {
        alert("Sorry invalid auctionId or bidOffer!");
    }
    else
    {
       // alert(auctionId + " " + bidOffer + "\n" + verificationToken);
        var bid = 
        {
            auctionId : auctionId,
            bidOffer : bidOffer
            // "__RequestVerificationToken" : verificationToken
        };
        $.ajax ({  
            type: "POST", 
            url: "/Auction/PlaceBid",
            data: {
                "auctionId" : auctionId,
                "bidOffer" : bidOffer,
                "__RequestVerificationToken" : verificationToken
            },
            dataType: "json",
            success: function ( response ) {
                // alert("Success occured!\n" + response);
                $(".closeTime"+auctionId).val(response.newCloseTime);
                $(".currentPrice"+auctionId).val(response.newCurrentPrice);
                $(".bidder"+auctionId).val(response.bidder);
                // alert(" " + response.auctionId + " "  + response.bidder + " " + response.newCurrentPrice + " " + response.newCloseTime);
                connection.invoke("NewBidOffered",parseInt(response.auctionId), parseInt(response.newCurrentPrice), response.bidder, response.newCloseTime);

            },
            error: function ( response ) {
                // alert("An error occured\n" + response);
                //alert (response.responseText) 
            }
        });   

     



    }
        

    

}

function collectBidOffer()
{
    var auctionId = $("#auctionId").val();
    var bidOffer = $("#bidOffer").val();

    if(bidOffer==null || auctionId==null)
    {
        alert("Error invalid input!");
    }
    else
    {
        auctionId = parseInt(auctionId);
        bidOffer = parseInt(bidOffer);
        if(bidOffer<=0 || auctionId<=0)
        {
            alert("Error invalid input!");
            $("#bidOffer").val(1);
        }
        else
        {   
            bid(auctionId, bidOffer);
        }
    }
}


function countDown()
{
    var i;
    for(i=1; i<=12; i++)
    {
        var string = $("#closeTime"+i).val();
        if(string==null)
            continue;
        
           
        var array = string.split(",");
    

        // alert(array[0] + " " + array[1] + " " + array[2] + " " + array[3] + " " + array[4] + " " +array[5] + " " +0);
        var now = new Date();
        // var eventDate = new Date(parseInt(array[0]), parseInt(array[1]), parseInt(array[2]), parseInt(array[3]), parseInt(array[4]), parseInt(array[5]), 0);
        var eventDate = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5]);


        //Jebali vas nulti meszeci mamu vam jebem, picka vam amterina, NULTI MESEC, mrs 


       

        var currentTime = now.getTime();
        var eventTime = eventDate.getTime();

        var remTime = eventTime - currentTime;

        // alert(currentTime + "\n" + eventTime);

        if(i==1){
            console.log(array[0], array[1], array[2], array[3], array[4], array[5]);

            console.log(eventDate);

            console.log(now);

            console.log(remTime);

            // console.log(eventDate - now);

            console.log(eventTime - currentTime);
        }

        // alert(remTime);
        if(eventTime - currentTime  < 0)
        {
            // alert("what?");
            var auctionId = $("#auctionId"+i).val();
            auctionId = parseInt(auctionId);
            closeAuction(auctionId);
            continue;
        }

        

        var s = Math.floor(remTime / 1000);
        var m = Math.floor(s / 60);
        var h = Math.floor(m / 60);
        var d = Math.floor(h / 24);

        h %= 24;
        m %= 60;
        s %= 60;
        d %= 30;

        // h = h - 1 < 0 ? 0 : h-1; //Iz nekog razloga zuri 1h

    

        // var d = Math.floor(remTime / (1000*60*60*24));
        // var h = Math.floor( (remTime % (1000*60*60*24)) / (1000*60*60) );
        // var m = Math.floor( (remTime % (1000*60*60) ) / (1000*60) );
        // var s = Math.floor( (remTime % (1000*60) ) / 1000);

        h = (h<10) ? "0" + h : h;
        m = (m<10) ? "0" + m : m;
        s = (s<10) ? "0" + s : s;
        d = (d < 10) ? "0" + d : d;

        $("#time"+i).text( d + ":" + h + ":" + m + ":" + s );

    }


    
}
setInterval(countDown,1000);


function closeAuction(auctionId){
    var aId = parseInt(auctionId);
    var verificationToken = $("input[name='__RequestVerificationToken']").val( );
    $.ajax ({  
        type: "POST", 
        url: "/Auction/CloseAuction",
        data: {
            "auctionId" : aId,
            "__RequestVerificationToken" : verificationToken
        },
        dataType: "json",
        success: function ( response ) {
            // alert("Success occured!\n" + response);
            $("#state"+auctionId).html("<div class='list-group-item' style='text-align: center;'>" + response.newState + "</div>");
            $("#bidButton"+auctionId).val("<button type='button' class='btn btn-md btn-outline-secondary disabled>&nbsp;Bid&nbsp;</button>");

            // alert(" " + response.auctionId + " "  + response.bidder + " " + response.newCurrentPrice + " " + response.newCloseTime);
            connection.invoke("AuctionClosed",parseInt(response.auctionId), response.newState);

        },
        error: function ( response ) {
            // alert("An error occured\n" + response);
            //alert (response.responseText) 
        }
    });   

}



//SignalR sheet

connection.on(
    "updateAuction", 
    function (auctionId, newCurrentPrice, winnerUsername, newCloseDate){

        var Id = parseInt(auctionId);
        var price = parseInt(newCurrentPrice);

        $(".closeTime"+Id).val(newCloseDate);
        $(".currentPrice"+Id).text(price);
        $(".bidder"+Id).text(winnerUsername);



    }

);
connection.on(
    "closeAuction",
    function (auctionId, newState){

        var Id = parseInt(auctionId);

        $("#state"+Id).html("<div class='list-group-item' style='text-align: center;'>" + newState + "</div>");
        $("#bidButton"+Id).val("<button type='button' class='btn btn-md btn-outline-secondary' disabled>&nbsp;Bid&nbsp;</button>");

    }


);


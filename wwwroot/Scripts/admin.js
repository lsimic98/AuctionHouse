function banUser(username)
{
    var verificationToken = $("input[name='__RequestVerificationToken']").val ( ) //Znaci ovde ga dohvatimo, on se nalazi na svakoj starnici

    $.ajax ({  
        type: "POST", 
        url: "/User/BanUser", 
        data: { 
            "username": username, 
            "__RequestVerificationToken" : verificationToken 
        },
        dataType: "text",
        success: function ( response ) {
            $("#"+username).html(response)
        },
        error: function ( response ) {
            alert ( response ) 
        }
    })

}
function unbanUser(username)
{
    var verificationToken = $("input[name='__RequestVerificationToken']").val ( ) //Znaci ovde ga dohvatimo, on se nalazi na svakoj starnici

    $.ajax ({  
        type: "POST", 
        url: "/User/UnbanUser", 
        data: { 
            "username": username, 
            "__RequestVerificationToken" : verificationToken 
        },
        dataType: "text",
        success: function ( response ) {
            $("#"+username).html(response);
        },
        error: function ( response ) {
            alert ( "User with username: "+username+" does not exists!" ) ;
        }
    })

}
function deleteAuction(id)
{
    if (confirm("Are you sure you want permanently delete this auction?")) {
        var verificationToken = $("input[name='__RequestVerificationToken']").val ( ); 
        $.ajax ({  
            type: "POST", 
            url: "/Auction/DeleteAuctionAdmin", 
            data: { 
                "id": parseInt(id), 
                "__RequestVerificationToken" : verificationToken 
            },
            success: function ( response ) {           
                alert ( "You deleted your auction successfully!" );
                location.reload(); 
            },
            error: function ( response ) {
                alert ( "Error, you can only delete auction in Draft state!" ); 
            }
        });
        
      }
}
function validateAuction(id)
{
    if (confirm("Are you sure you want to validate this auction?")) {
        var verificationToken = $("input[name='__RequestVerificationToken']").val ( ); 
        $.ajax ({  
            type: "POST", 
            url: "/Auction/ValidateAuction", 
            data: { 
                "id": parseInt(id), 
                "__RequestVerificationToken" : verificationToken 
            },
            success: function ( response ) {           
                alert ( "You validated this auction successfully!" );
                location.reload(); 
            },
            error: function ( response ) {
                alert ( "Error, you can only validate auction in Draft state!" ); 
            }
        });
        
      }
}


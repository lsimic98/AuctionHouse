function countDown()
{
    var i;
    for(i=1; i<=12; i++)
    {
        var string = $("#closeTime"+i).val();
        if(string==null)
            break;
        
           
        var array = string.split(",");
    

        // alert(array[0] + " " + array[1] + " " + array[2] + " " + array[3] + " " + array[4] + " " +array[5] + " " +0);
        var now = new Date();
        // var eventDate = new Date(parseInt(array[0]), parseInt(array[1]), parseInt(array[2]), parseInt(array[3]), parseInt(array[4]), parseInt(array[5]), 0);
        var eventDate = new Date(array[0], array[1], array[2], array[3], array[4], array[5], 0);
       

        

        var currentTime = now.getTime();
        var eventTime = eventDate.getTime();

        var remTime = eventTime - currentTime;

        

        var s = Math.floor(remTime / 1000);
        var m = Math.floor(s / 60);
        var h = Math.floor(m / 60);
        var d = Math.floor(h / 24);

        h %= 24;
        m %= 60;
        s %= 60;
        d %= 30;

        h = h - 1 < 0 ? 0 : h-1; //Iz nekog razloga zuri 1h

    

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


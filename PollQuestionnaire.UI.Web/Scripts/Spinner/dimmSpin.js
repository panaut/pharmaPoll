var dimSpinner = new DimSpinner();

var spinnerContainer = document.createElement('div');
spinnerContainer.id = 'spinner';
spinnerContainer.style.zIndex = 2e9;
//2147483647
document.getElementsByTagName('body')[0].appendChild(spinnerContainer);

function spinStart() {
    dimSpinner.spin(spinnerContainer);
}

function spinStop() {
    dimSpinner.stop();
}

function DimSpinner() {

    if (document.getElementsByClassName('LockOn') != undefined) {
        var lockOnStyle = '.LockOn { display: block; visibility: visible; position: fixed; z-index: 999; top: 0%; left: 0%; width: 100%; height: 100%; background-color: #000; text-align: center; padding-top: 20%; filter: alpha(opacity=75); opacity: 0.50; }';
        var lockOffStyle = '.LockOff { display: none; visibility: hidden; }';

        var styleElement = document.createElement('style');
        styleElement.type = 'text/css';
        styleElement.innerHTML = lockOnStyle + ' ' + lockOffStyle;
        document.getElementsByTagName('head')[0].appendChild(styleElement);
    }

    //if (document.getElementsByClassName('LockOff') != undefined) {
    //    var lockOffStyle = '.LockOff { display: none; visibility: hidden; }';
    //    var LockOff = document.createElement('style');
    //    LockOff.type = 'text/css';
    //    LockOff.innerHTML = lockOffStyle;
    //    document.getElementsByTagName('head')[0].appendChild(LockOff);
    //}

    var opts = {
        lines: 12             // The number of lines to draw
                , length: 7             // The length of each line
                , width: 5              // The line thickness
                , radius: 10            // The radius of the inner circle
                , scale: 1.0            // Scales overall size of the spinner
                , corners: 1            // Roundness (0..1)
                , color: '#ccc'         // #rgb or #rrggbb
                , opacity: 1 / 4          // Opacity of the lines
                , rotate: 0             // Rotation offset
                , direction: 1          // 1: clockwise, -1: counterclockwise
                , speed: 1              // Rounds per second
                , trail: 100            // Afterglow percentage
                , fps: 20               // Frames per second when using setTimeout()
                , zIndex: 2e9             // Use a high z-index by default
                , className: 'spinner'  // CSS class to assign to the element
                , top: '50%'            // center vertically
                , left: '50%'           // center horizontally
                , shadow: false         // Whether to render a shadow
                , hwaccel: false        // Whether to use hardware acceleration (might be buggy)
                , position: 'absolute'  // Element positioning
    }

    var spinner = new Spinner(opts)

    this.spin = function (element) {
        element.className = 'LockOn';
        spinner.spin(element);
        spinner.targetElement = element;
    }

    this.stop = function () {
        spinner.targetElement.className = 'LockOff';
        spinner.stop();
    }
}
window.ShowToastr = (type, message) => {
    if(type === 'success'){
        toastr.success(message, 'Successful', {timeOut: 5000})
    }
    if(type === 'failed'){
        toastr.error(message, 'Failed', {timeOut: 5000})
    }
    if(type === 'notfound'){
        toastr.warning(message, 'Warning', {timeOut: 5000})
    }

    if(type === 'warning'){
        toastr.warning(message, 'Warning', {timeOut: 5000})
    }
}
function clearInput(inputId) {
    var input = document.querySelector("#" + inputId);
    if (input) {
        input.value = "";
    }
}

function mostrarToast(icon, title) {
    Toast.fire({
        icon: icon,
        title: title
    });
}
const Toast = swal.mixin({
    toast: true,
    position: 'bottom-start',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', swal.stopTimer)
        toast.addEventListener('mouseleave', swal.resumeTimer)
    }
})

function mostrarMensajeConfirmacion(title, text, icon) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si'
        }).then((result) => {
            resolve(result.isConfirmed)
        })
    })
}

async function mostrarMensajeTextoConfirmacion(title, text, value) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            input: 'text',
            inputLabel: text,
            inputValue: value,
            showCancelButton: true,
            inputValidator: (value) => {
                if (!value) {
                    return 'Por favor ingrese la información!'
                }
            }
        }).then((result) => {
            resolve(result.value)
        })
    })
}

function showConfirm(title, text, value) {
    return new Promise(resolve => {
        Swal.fire({
            title: title,
            text: text,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Aceptar'
        }).then((result) => {
            if (result.isConfirmed) {
                return true;
            } else {
                return false;
            }
        });
    });
}

async function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}
function saveAsFile(filename, bytesBase64) {
  if (navigator.msSaveBlob) { // Can download notification bar be displayed
    let data = window.atob(bytesBase64); // Decode
    let bytes = new Uint8Array(data.length);
    for (let i = 0; i < data.length; i++) {
      bytes[i] = data.charCodeAt(i);
    }
    let blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
    navigator.msSaveBlob(blob, filename);
  }
  else {
    // Support for Firefox
    let link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); 
    link.click();
    document.body.removeChild(link);
  }
}

function ScrollTo(id) {
  document.getElementById(id).scrollIntoView();
}
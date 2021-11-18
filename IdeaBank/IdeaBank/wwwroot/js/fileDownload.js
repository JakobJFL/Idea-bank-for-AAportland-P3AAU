function downloadCSV(context) {
    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,%EF%BB%BF' + encodeURI(context);
    hiddenElement.target = '_blank';
    hiddenElement.download = 'ideas.csv';
    hiddenElement.click();
}
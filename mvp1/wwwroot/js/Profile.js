function Select(e) {
    let sel = document.getElementById('sel-list');
    let li = document.createElement('li');
    li.innerHTML = e.value;
    sel.appendChild(li);
}

function Merge() {
    const lis = document.getElementById('sel-list').getElementsByTagName('li');
    let len = "";
    for (let i = 0; i <= lis.length - 1; i++) {
        len = len + ';' + lis[i].innerHTML;
    }
    console.log(len);
    window.location.replace(window.location.origin + '/Super/UserMarketMerge/' + document.getElementById("userid").ariaValueMax + '?len=' + len;
}
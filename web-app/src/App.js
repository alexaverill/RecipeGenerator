import logo from './logo.svg';
import { useState, useEffect } from "react";
import './App.css';
function App() {
 const [content,setContent] = useState("");
  useEffect(()=>{
    window.addEventListener('loadRecipe', loadRecipe);
    return ()=>window.removeEventListener('loadRecipe',loadRecipe);
  })
  useEffect(()=>{
    window.addEventListener('clearRecipe', clearRecipe);
    return ()=>window.removeEventListener('loadRecipe',clearRecipe);
  })
  async function loadRecipe(event){
    setContent(event.detail.data);
    document.body.classList.add("ready");
    
  }
  function clearRecipe(event){
    setContent("");
    document.body.classList.remove("ready")
  }
  
  return (
    <div className="App" dangerouslySetInnerHTML={{__html:content}}>
    </div>
  );
}

export default App;

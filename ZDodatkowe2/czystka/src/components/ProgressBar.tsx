import React from "react";
import styled from "styled-components";


interface CharacterWindowProps {
    percentage: number
}

const Wrapper = styled.div`
    margin: 10px;
    border: solid;
    width:50%;
    border-radius: 10px;
`;


const ProgressBar = ({percentage}: CharacterWindowProps) => {
    return (
        <Wrapper>
            <div style={{backgroundColor: 'blue',
                        width: percentage + '%',
                        height: 20 + 'px'}}>

            </div>
        </Wrapper>
    )
};


export default ProgressBar;
import React from "react";
import styled from "styled-components";
import { ProgressBar } from "react-bootstrap";

const Toolbar = styled.div`
`;

interface InfoToolbarProps {
    storyProgress: number
}

const InfoToolbar = (props: InfoToolbarProps) => {
    return (
        <ProgressBar now={50}/>

    )
};


export default InfoToolbar;
import { useState } from "react";
import { Select, Stack, Option } from "@mui/joy";
import { useCom, useSpices } from "../MainProvider";

function Com() {
  const { connected, getComPorts, connectPort, disconnectPort, writeMessage } = useCom();
  const [ comPorts, setComPorts ] = useState(['COM3', 'COM7']);

  return (
    <Stack spacing={1} direction="row" sx={{height:'100%',width:'100%'}}>
      <Select
        placeholder="Choose one…"
        variant="soft"
      >
        {comPorts.map((comPort,i) => (
          <Option value={i}>{comPort}</Option>
        ))}
      </Select>
    </Stack>
  )
}

export default Com;